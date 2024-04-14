namespace SurveySystem.Aplication.Requests.Characteristic
{
    public class PatchCharacteristicReqiest
    {
        public Guid CharactetisticId { get; set; }
        public string Description { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
    }
}
