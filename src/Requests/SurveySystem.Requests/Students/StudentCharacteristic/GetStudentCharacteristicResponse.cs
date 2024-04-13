using SurveySystem.Domain.Enums;

namespace SurveySystem.Requests.Students.StudentCharacteristic
{
    public class GetStudentCharacteristicResponse
    {
        public List<Tuple<string, CharacteristicMeasure>> PersonalCharacteristics { get; set; } = new();
        public List<Tuple<string, CharacteristicMeasure>> Subjects { get; set; } = new();
    }
}
