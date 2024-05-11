namespace SurveySystem.Requests.Questions.GetQuestionList
{
    public class GetQuestionListResponse
    {
        public IEnumerable<QuestionDTO> Questions { get; set; }
    }
}
