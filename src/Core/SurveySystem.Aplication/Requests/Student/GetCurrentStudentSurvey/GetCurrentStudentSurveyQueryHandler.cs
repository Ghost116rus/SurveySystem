using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.Entities.Users;
using SurveySystem.Domain.Enums;
using SurveySystem.Domain.Exceptions;
using SurveySystem.Requests.Students.GetCurrentStudentSurvey;

namespace SurveySystem.Aplication.Requests.Student.GetCurrentStudentSurvey
{
    public class GetCurrentStudentSurveyQueryHandler : IRequestHandler<GetCurrentStudentSurveyQuery, GetCurrentStudentSurveyResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;
        private readonly IQuestionService _questionService;

        public GetCurrentStudentSurveyQueryHandler(IDbContext dbContext, IUserContext userContext, IQuestionService questionService)
        {
            _dbContext = dbContext;
            _userContext = userContext;
            _questionService = questionService;
        }

        public async Task<GetCurrentStudentSurveyResponse> Handle(GetCurrentStudentSurveyQuery request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var studentProgress = await _dbContext.SurveyProgress
                .Include(x => x.Survey).ThenInclude(s => s.Questions).FirstOrDefaultAsync(x => x.Id == request.StudentSurveyId)
                ?? throw new ExceptionBase("Заданный прогресс не найден");

            if (_userContext.CurrentUserId != studentProgress.StudentId &&
                _userContext.CurrentUserRoleName != Enum.GetName(Role.Administrator))
                throw new ForbibenException("У вас нет доступа");

            var allAnswers = await _dbContext.StudentAnswers
                .Include(sA => sA.Answer)
                .ThenInclude(a => a.Question)
                .ThenInclude(q => q.Answers)
                .Where(sA => sA.SurveyProgressId == studentProgress.Id)
                .OrderBy(sA => sA.ModifiedOn)
                .ToListAsync();

            var allAnswersDict = allAnswers.GroupBy(sA => sA.IsActual)
                .ToDictionary(x => x.Key, x => x.ToList());

            List<CurrentStudentSurveyTestQuestionDTO> history = getQuestionList(allAnswersDict, false);
            List<CurrentStudentSurveyTestQuestionDTO> actualAnswers = getQuestionList(allAnswersDict, true);
            CurrentStudentSurveyTestQuestionDTO? currentQuestion = await _questionService.GetCurrentQuestionDTO(studentProgress);

            return new GetCurrentStudentSurveyResponse()
            {
                IsRepetable = studentProgress.Survey!.IsRepetable,
                IsCompleted = studentProgress.IsCompleted,
                History = history,
                ActualAnswers = actualAnswers,
                CurrentQuestion = currentQuestion
            };
        }

        private List<CurrentStudentSurveyTestQuestionDTO> getQuestionList(Dictionary<bool, List<StudentAnswer>> allAnswers, bool v)
        {
            if (!allAnswers.ContainsKey(v))
                return new();

            var collection = allAnswers[false];
            return collection.GroupBy(x => x.Answer!.Question!)
                .Select(x => new CurrentStudentSurveyTestQuestionDTO()
                {
                    Id = x.Key.Id,
                    QuestionText = x.Key.Text,
                    MaxCountOfAnswers = x.Key.MaxCountOfAnswers,
                    Type = x.Key.Type,
                    AnswerTime = x.Key.Answers!.First(a => x.Any(sA => sA.AnswerId == a.Id))?.ModifiedOn.ToString(),
                    Answers = x.Key!.Answers!.Select(a => new CurrentStudentSurveyQuestionAnswerDTO()
                    {
                        Id = a.Id,
                        Text = a.Text,
                        IsSelected = x.Any(sA => sA.AnswerId == a.Id)
                    }).ToList()
                }).ToList();
        }
    }
}
