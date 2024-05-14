using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.DomainEvents;
using SurveySystem.Domain.Entities.Users;
using SurveySystem.Domain.Exceptions;

namespace SurveySystem.Aplication.DomainEventsHandlers
{
    public class StudentEndSurveyEventHandler : INotificationHandler<StudentEndSurveyEvent>
    {
        private readonly IDbContext _dbContext;

        public StudentEndSurveyEventHandler(IDbContext dbContext)
            => _dbContext = dbContext;

        public async Task Handle(StudentEndSurveyEvent notification, CancellationToken cancellationToken)
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

            Dictionary<Guid, StudentCharacteristic> studentCharacteristics = new();

            await UpdateStudentCharacteristicsAsync(surveyProgress, characteristicsIds, studentCharacteristics);

            // пересчет характеристик студента
            foreach (var studentAnswer in actualAnswers)            
                foreach (var answerCharacteristicValues in studentAnswer.Answer.AnswerCharacteristicValues)
                {
                    var studentCharacteristic = studentCharacteristics[answerCharacteristicValues.CharacteristicId];
                    studentCharacteristic.Value += answerCharacteristicValues.Value;
                }

            await _dbContext.SaveChangesAsync();
        }

        private async Task UpdateStudentCharacteristicsAsync(StudentSurveyProgress surveyProgress, HashSet<Guid> characteristicsIds,
            Dictionary<Guid, StudentCharacteristic> studentCharacteristics)
        {
            List<StudentCharacteristic> newStudentCharacteristics = new();

            foreach (var characteristic in surveyProgress.Student.StudentCharacteristics)
            {
                var id = characteristic.CharacteristicId;
                if (characteristicsIds.Contains(id))
                {
                    characteristicsIds.Remove(id);
                    studentCharacteristics.Add(id, characteristic);
                }
            }

            if (characteristicsIds.Count > 0)
            {
                var characteristicsFromDb = await _dbContext.Characteristics
                    .Where(x => characteristicsIds.Contains(x.Id)).ToListAsync();
                var student = surveyProgress.Student;

                foreach (var characteristic in characteristicsFromDb)
                {
                    var newStudentCharacteristic = new StudentCharacteristic(student, characteristic);
                    newStudentCharacteristics.Add(newStudentCharacteristic);
                    studentCharacteristics.Add(characteristic.Id, newStudentCharacteristic);
                }
            }

            if (newStudentCharacteristics.Count > 0)
                await _dbContext.StudentCharacteristic.AddRangeAsync(newStudentCharacteristics);
        }
    }
}
