using SurveySystem.Domain.Enums;
using SurveySystem.Requests.Tags;


namespace SurveySystem.Requests.Questions
{
    public class QuestionDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public int MaxCountOfAnswers { get; set; }
        public QuestionType Type { get; set; }

        public IEnumerable<AnswerDTO>? Answers { get; set; }
        public IEnumerable<QuestionEvaluationCriteriaDTO>? Criteries { get; set; }
        public IEnumerable<TagDTO> Tags { get; set; }

    }
}
