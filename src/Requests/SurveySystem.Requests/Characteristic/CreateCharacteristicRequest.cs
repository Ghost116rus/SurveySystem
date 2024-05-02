using SurveySystem.Domain.Enums;

namespace SurveySystem.Aplication.Requests.Characteristic
{
    public class CreateCharacteristicRequest
    {
        public string PositiveDescription { get; set; }
        public string NegativeDescription { get; set; }

        public CharacteristicType CharacteristicType { get; set; }

        public double MinValue { get; set; }
        public double MaxValue { get; set; }
    }
}
