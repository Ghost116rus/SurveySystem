using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.Entities.Users;
using SurveySystem.Domain.Exceptions;
using SurveySystem.Requests.Students.AnswerInSurvey;

namespace SurveySystem.Aplication.Requests.Student.AnswerInSurvey
{
    public class AddNewAnswerForStudentSurveyCommandHandler :IRequestHandler<AddNewAnswerForStudentSurveyCommand, AddNewAnswerForStudentSurveyResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;
        private readonly IQuestionService _questionService;

        public AddNewAnswerForStudentSurveyCommandHandler(IDbContext dbContext, IQuestionService questionService, IUserContext userContext)
        {
            _dbContext = dbContext;
            _questionService = questionService;
            _userContext = userContext;
        }

        public async Task<AddNewAnswerForStudentSurveyResponse> Handle(AddNewAnswerForStudentSurveyCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var surveyProgress = await _dbContext.SurveyProgress
                .Include(x => x.Survey)
                .ThenInclude(s => s!.Questions)
                .FirstOrDefaultAsync(x => x.Id == request.StudentSurveyProgressId)
                ?? throw new NotFoundException("Не был найден данный прогресс студента");

            if (request.AnswersId.Count < 1)
                throw new BadDataException("Нет ответов студента");

            if (surveyProgress.StudentId != _userContext.CurrentUserId)
                throw new ForbibenException("У вас нет возможности отвечать на этот опрос");

            var answers = await _dbContext.Answers.Include(a => a.Question)
                .Where(x => request.AnswersId.Contains(x.Id)).ToListAsync();

            if (answers is null || answers.Count == 0)
                throw new NotFoundException("Не были найдены заданные ответы");

            if (answers.First().Question!.MaxCountOfAnswers < request.AnswersId.Count)
                throw new BadDataException("Максимальное количество ответов было превышено");

            var studentAnswers = answers.Select(x => new StudentAnswer(surveyProgress, x)).ToList();
            surveyProgress.UpdatePosition(surveyProgress.CurrentPostion + 1, surveyProgress.Survey!.Questions!.Count);

            await _dbContext.StudentAnswers.AddRangeAsync(studentAnswers);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var currentQuestion = await _questionService.GetCurrentQuestionDTO(surveyProgress);

            return new AddNewAnswerForStudentSurveyResponse()
            {
                CurrentQuestion = currentQuestion,
                IsCompleted = surveyProgress.IsCompleted,
            };
        }
    }
}
