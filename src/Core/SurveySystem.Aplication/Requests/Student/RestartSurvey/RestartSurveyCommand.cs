using MediatR;
using SurveySystem.Requests.Students.RestartSruventSurvey;

namespace SurveySystem.Aplication.Requests.Student.RestartSurvey
{
    public class RestartSurveyCommand : RestartSruventSurveyRequest, IRequest<RestartSruventSurveyResponse>
    {
    }
}
