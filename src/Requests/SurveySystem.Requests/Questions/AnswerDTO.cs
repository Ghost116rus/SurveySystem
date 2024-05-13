namespace SurveySystem.Requests.Questions
{
    public class AnswerDTO
    {
        public string Text { get; set; }
        public IEnumerable<AnswerCharactersticValueDTO> CharacteristicsValues { get; set; }
        public int PositionInQuestion { get; set; } = -1;
    }

    public class AnswerDTOWId : AnswerDTO
    {
        public Guid Id { get; set; }    
    }
}
