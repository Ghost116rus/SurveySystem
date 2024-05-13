using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.Entities.Surveys;
using SurveySystem.Domain.Enums;
using SurveySystem.Domain.Exceptions;

namespace SurveySystem.Aplication.Requests.Surveys.Questions.Create
{
    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, Guid>
    {
        private IDbContext _dbContext;

        public CreateQuestionCommandHandler(IDbContext dbContext) 
            => _dbContext = dbContext;  

        public async Task<Guid> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var tags = await _dbContext.Tags.Where(t => request.TagIds.Contains(t.Id)).ToListAsync();

            var newQuestion = new Question(request.Type, request.Text, request.MaxCountOfAnswers, tags);

            if (request.Type == QuestionType.Open)
            {
                foreach (var item in request.NewCriteries)
                {
                    var criteria = new QuestionEvaluationCriteria(item);
                    criteria.UpdateQuestionsList(new List<Question>(1) { newQuestion });
                    _dbContext.QuestionEvaluationCriteries.Add(criteria);
                }

                var existedCriteries = await _dbContext.QuestionEvaluationCriteries
                    .Where(x => request.ExistedCriteries.Contains(x.Id)).ToListAsync();

                foreach (var item in existedCriteries)
                {
                    var questionList = item.Questions.ToList();
                    questionList.Add(newQuestion);
                    item.UpdateQuestionsList(questionList);
                }
            }
            else if (request.Type == QuestionType.Alternative || request.Type == QuestionType.NonAlternative)
            {
                HashSet<Guid> characteristicsId = new();
                request.Answers.Select(a => a.CharacteristicsValues.Select(x => characteristicsId.Add(x.CharacteristicId)));

                var characteristics = await _dbContext.Characteristics.Where(x => characteristicsId.Contains(x.Id)).ToListAsync();
                var characteristicsDictionary = characteristics.ToDictionary(x => x.Id, x => x);

                foreach (var answer in request.Answers)
                {
                    var newAnswer = new Answer(answer.Text, newQuestion, answer.PositionInQuestion);
                    foreach (var item in answer.CharacteristicsValues)
                    {
                        var newAnswerValue = new AnswerCharacteristicValue(newAnswer,
                            characteristicsDictionary[item.CharacteristicId],
                            item.CharacteristicsValue);
                        _dbContext.AnswerCharacteristicValues.Add(newAnswerValue);
                    }
                    _dbContext.Answers.Add(newAnswer);
                }
            }
            else
                throw new BadDataException("Неправильный тип вопроса");

            await _dbContext.SaveChangesAsync();

            return newQuestion.Id;
        }
    }
}
