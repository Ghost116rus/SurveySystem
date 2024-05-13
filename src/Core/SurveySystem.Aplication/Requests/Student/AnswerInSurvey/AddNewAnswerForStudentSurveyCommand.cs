using MediatR;
using SurveySystem.Requests.Students.AnswerInSurvey;

namespace SurveySystem.Aplication.Requests.Student.AnswerInSurvey
{
    public class AddNewAnswerForStudentSurveyCommand : AddNewAnswerForStudentSurveyRequest, IRequest<AddNewAnswerForStudentSurveyResponse>
    {
    }
}
