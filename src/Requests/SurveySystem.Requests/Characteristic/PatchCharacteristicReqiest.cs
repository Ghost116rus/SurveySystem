namespace SurveySystem.Aplication.Requests.Characteristic
{
    public class PatchCharacteristicReqiest
    {
        public Guid CharactetisticId { get; set; }
        public string PositiveDescription { get; set; }
        public string NegativeDescription { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
    }
}
