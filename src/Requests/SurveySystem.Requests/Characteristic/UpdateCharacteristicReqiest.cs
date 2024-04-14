using SurveySystem.Domain.Enums;

namespace SurveySystem.Aplication.Requests.Characteristic
{
    public class UpdateCharacteristicReqiest : PatchCharacteristicReqiest
    {
        public CharacteristicType CharacteristicType { get; set; }
    }
}
