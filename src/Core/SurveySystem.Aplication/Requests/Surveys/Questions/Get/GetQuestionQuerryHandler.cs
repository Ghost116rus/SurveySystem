using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Extenstions;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.Exceptions;
using SurveySystem.Requests.Questions;
using SurveySystem.Requests.Questions.GetQuestionList;

namespace SurveySystem.Aplication.Requests.Surveys.Questions.Get
{
    public class GetQuestionQuerryHandler : IRequestHandler<GetQuestionQuerry, GetQuestionListResponse>
    {
        private readonly IDbContext _dbContext;

        public GetQuestionQuerryHandler(IDbContext dbContext) 
            => _dbContext = dbContext;

        public async Task<GetQuestionListResponse> Handle(GetQuestionQuerry response, CancellationToken cancellationToken)
        {
            var questions = await _dbContext.Questions
                .Include(q => q.Answers)!.ThenInclude(a => a.AnswerCharacteristicValues)
                .Include(q => q.Criteries).Include(q => q.Tags)
                .FilterByTags(response.Tags)
                .Select(q => new QuestionDTO()
                {
                    Id = q.Id,
                    Text = q.Text,
                    MaxCountOfAnswers = q.MaxCountOfAnswers,
                    Type = q.Type,
                    Answers = q.Answers == null ? null : q.Answers.Select(a => new AnswerDTO() 
                    { 
                        CharacteristicsValues = a.AnswerCharacteristicValues
                        .Select(a => new AnswerCharactersticValueDTO()
                        { 
                            CharacteristicId = a.CharacteristicId, 
                            CharacteristicsValue = a.Value
                        }) 
                    }),
                    Criteries = q.Criteries == null ? null : q.Criteries.Select(c => new QuestionEvaluationCriteriaDTO() 
                    { 
                        Criteria = c.Criteria, 
                        Id = c.Id
                    })
                }).ToListAsync();

            if (questions is null || questions.Count < 1)
                throw new NotFoundException("Вопросы не были найдены");

            return new GetQuestionListResponse() { Questions = questions };
        }
    }
}
