using SurveySystem.Domain.Enums;

namespace SurveySystem.Requests.Characteristic
{
    public class CharacteristicDTO
    {
        public Guid Id { get; set; }
        public string PositiveDescription { get; set; } = default!;
        public string NegativeDescription { get; set; } = default!;
        public CharacteristicType CharacteristicType { get; set; } = default!;
        public string CharacteristicTypeDescription { get; set; } = default!;
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
    }

}
