using SurveySystem.Domain.Entities.Surveys;

namespace SurveySystem.Domain.Interfaces
{
    public interface IAnswersService
    {
        public List<Answer> GetDefaultAnswers(Question question);
        public List<AnswerCharacteristicValue> GetDefaultAnswerCharacteristicValuesForSurvey(
            List<Tuple<Characteristic, List<Answer>>> characteristicsAndAnswers);
    }
}
