using SurveySystem.Domain.Enums;

namespace SurveySystem.Aplication.Requests.Characteristic
{
    public class CreateCharacteristicRequest
    {
        public string Description { get; set; }

        public CharacteristicType CharacteristicType { get; set; }

        public double MinValue { get; set; }
        public double MaxValue { get; set; }
    }
}
