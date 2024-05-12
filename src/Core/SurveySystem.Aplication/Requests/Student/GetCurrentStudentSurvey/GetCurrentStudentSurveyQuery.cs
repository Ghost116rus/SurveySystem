using MediatR;
using SurveySystem.Requests.Students.GetCurrentStudentSurvey;

namespace SurveySystem.Aplication.Requests.Student.GetCurrentStudentSurvey
{
    public class GetCurrentStudentSurveyQuery : GetCurrentStudentSurveyRequest, IRequest<GetCurrentStudentSurveyResponse>
    {
    }
}
