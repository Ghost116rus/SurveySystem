using SurveySystem.Domain.Enums;

namespace SurveySystem.Requests.Students.StudentCharacteristic
{
    public class GetStudentProfileResponse
    {
        public List<StudentCharacteristicDTO> PersonalCharacteristics { get; set; } = new();
        public List<StudentCharacteristicDTO> Subjects { get; set; } = new();
    }
}
