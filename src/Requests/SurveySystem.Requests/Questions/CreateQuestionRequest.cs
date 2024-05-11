using SurveySystem.Domain.Enums;
using SurveySystem.Requests.Tags;

namespace SurveySystem.Requests.Questions
{
    public class CreateQuestionRequest
    {
        public string Text { get; set; }
        public int MaxCountOfAnswers { get; set; }
        public QuestionType Type { get; set; }
        public IEnumerable<AnswerDTO> Answers { get; set; }
        public IEnumerable<string> NewCriteries { get; set; }
        public IEnumerable<Guid> ExistedCriteries { get; set; }
        public IEnumerable<Guid> TagIds { get; set; }
    }
}
