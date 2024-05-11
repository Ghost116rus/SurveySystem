namespace SurveySystem.Requests.Questions
{
    public class AnswerDTO
    {
        public string Text { get; set; }
        public IEnumerable<AnswerCharactersticValueDTO> CharacteristicsValues { get; set; }
    }

    public class AnswerDTOWId : AnswerDTO
    {
        public Guid Id { get; set; }    
    }
}
