using MediatR;
using SurveySystem.Requests.Students.StudentCharacteristic;


namespace SurveySystem.Aplication.Requests.Student.Characteristics.GetStudentCharacteristic
{
    public class GetOnlyPositiveCharacteristicInStudentProfileQuery : IRequest<GetStudentProfileResponse>
    {
    }
}
