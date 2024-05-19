using SurveySystem.Domain.Enums;

namespace SurveySystem.Requests.DigitalProfile
{
    public class GetDigitalProfileResponse
    {
        public List<StudentCharacteristicDTO> PersonalCharacteristics { get; set; } = new();
        public List<StudentCharacteristicDTO> Subjects { get; set; } = new();
    }
}
