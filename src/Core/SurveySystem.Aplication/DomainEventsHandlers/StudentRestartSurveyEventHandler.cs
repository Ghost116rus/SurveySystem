using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.DomainEvents;
using SurveySystem.Domain.Entities.Users;
using SurveySystem.Domain.Exceptions;

namespace SurveySystem.Aplication.DomainEventsHandlers
{
    public class StudentRestartSurveyEventHandler : INotificationHandler<StudentRestartSurveyEvent>
    {
        private readonly IDbContext _dbContext;

        public StudentRestartSurveyEventHandler(IDbContext dbContext)
            => _dbContext = dbContext;

        public async Task Handle(StudentRestartSurveyEvent notification, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(notification);

            var surveyProgress = await _dbContext.SurveyProgress
                .Include(sp => sp.Student)
                    .ThenInclude(st => st.StudentCharacteristics)
                        .ThenInclude(sc => sc.Characteristic)
                .Include(sp => sp.Answers)!
                    .ThenInclude(a => a.Answer)
                        .ThenInclude(a => a.AnswerCharacteristicValues)
                .FirstOrDefaultAsync(x => x.Id == notification.SurveyProgressId)
                ?? throw new ExceptionBase("Не был найден прогресс студента в доменном событии");

            var actualAnswers = surveyProgress.Answers!.Where(x => x.IsActual).ToList();

            if (actualAnswers.Count == 0)
                throw new ExceptionBase("невозможно подсчитать результаты опроса при отсутствии ответов");

            HashSet<Guid> characteristicsIds = new HashSet<Guid>();
            foreach (var item in actualAnswers)
            {
                var answerCharacteristics = item.Answer.AnswerCharacteristicValues.Select(x => x.CharacteristicId);
                foreach (var characteristicId in answerCharacteristics)
                    characteristicsIds.Add(characteristicId);
            }
            Dictionary<Guid, StudentCharacteristic> studentCharacteristics = surveyProgress.Student.StudentCharacteristics
                .Where(x => characteristicsIds.Contains(x.CharacteristicId))
                .ToDictionary(x => x.CharacteristicId, x => x);

            // вычет характеристик студента
            foreach (var studentAnswer in actualAnswers)
            {
                foreach (var answerCharacteristicValues in studentAnswer.Answer.AnswerCharacteristicValues)
                {
                    var studentCharacteristic = studentCharacteristics[answerCharacteristicValues.CharacteristicId];
                    studentCharacteristic.Value -= answerCharacteristicValues.Value;
                }
                studentAnswer.IsActual = false;
            }

            await _dbContext.SaveChangesAsync();
        }
        
    }
}
