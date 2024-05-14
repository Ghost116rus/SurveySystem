using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.Exceptions;
using SurveySystem.Requests.Students.RestartSruventSurvey;

namespace SurveySystem.Aplication.Requests.Student.RestartSurvey
{
    public class RestartSurveyCommandHandler : IRequestHandler<RestartSurveyCommand, RestartSruventSurveyResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;
        private readonly IQuestionService _questionService;

        public RestartSurveyCommandHandler(IDbContext dbContext, IUserContext userContext, IQuestionService questionService)
        {
            _dbContext = dbContext;
            _userContext = userContext;
            _questionService = questionService;
        }

        public async Task<RestartSruventSurveyResponse> Handle(RestartSurveyCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var surveyProgress = await _dbContext.SurveyProgress
                .Include(sP => sP.Survey)
                .FirstOrDefaultAsync(x => x.Id == request.StudentSurveyId)
                ?? throw new NotFoundException("Заданный прогресс не был найден");

            if (surveyProgress.StudentId != _userContext.CurrentUserId)
                throw new ForbibenException("Вы не имеете прав для данного действия");

            if (surveyProgress.Survey.IsRepetable is false)
                throw new BadDataException("Вы не можете начать заново опрос, у которого это запрещено");

            if (!surveyProgress.IsCompleted)
                throw new BadDataException("Вы не можете начать заново незаконченный опрос");

            surveyProgress.Restart();

            var currentQuestion = await _questionService.GetCurrentQuestionDTOAsync(surveyProgress);

            await _dbContext.SaveChangesAsync();

            return new RestartSruventSurveyResponse()
            {
                CurrentQuestion = currentQuestion
            };
        }

    }
}
