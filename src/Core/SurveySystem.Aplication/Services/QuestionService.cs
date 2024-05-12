using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.Entities.Users;
using SurveySystem.Domain.Exceptions;
using SurveySystem.Requests.Students.GetCurrentStudentSurvey;

namespace SurveySystem.Aplication.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IDbContext _dbContext;

        public QuestionService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CurrentStudentSurveyTestQuestionDTO?> GetCurrentQuestionDTO(StudentSurveyProgress studentProgress)
        {
            if (studentProgress.IsCompleted)
                return null;

            var testQuestion = studentProgress.Survey!
                .Questions!.First(x => x.Position == studentProgress.CurrentPostion) ??
                    throw new ExceptionBase("Текущая позиция не найдена в таблице позиций");

            var currentQuestionId = testQuestion.QuestionId;

            var currentQuestion = await _dbContext.Questions
                .Include(q => q.Answers)
                .Where(x => x.Id == currentQuestionId)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("Текущий вопрос не был найден");

            return new CurrentStudentSurveyTestQuestionDTO()
            {
                Id = currentQuestionId,
                MaxCountOfAnswers = currentQuestion.MaxCountOfAnswers,
                QuestionText = currentQuestion.Text,
                Type = currentQuestion.Type,
                AnswerTime = "",
                Answers = currentQuestion.Answers!.Select(x => new CurrentStudentSurveyQuestionAnswerDTO()
                {
                    Id = x.Id,
                    Text = x.Text,
                }).ToList(),
            };
        }
    }
}
