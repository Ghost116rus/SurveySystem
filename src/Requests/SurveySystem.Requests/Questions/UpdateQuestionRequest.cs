namespace SurveySystem.Requests.Questions
{
    public class UpdateQuestionRequest : CreateQuestionRequest
    {
        public Guid Id { get; set; }
        public IEnumerable<AnswerDTOWId> ExistedAnswers { get; set; }
    }
}
