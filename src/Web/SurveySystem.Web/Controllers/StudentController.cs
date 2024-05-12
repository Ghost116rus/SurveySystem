using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveySystem.Aplication.Requests.Student.Characteristics.GetStudentCharacteristic;
using SurveySystem.Aplication.Requests.Student.GetCurrentStudentSurvey;
using SurveySystem.Aplication.Requests.Student.StudentProgress;
using SurveySystem.Requests.Students;
using SurveySystem.Requests.Students.GetCurrentStudentSurvey;
using SurveySystem.Requests.Students.StudentCharacteristic;

namespace SurveySystem.Web.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : BaseController
    {
        [HttpGet]
        public async Task<GetPositiveStudentCharacteristicResponse> GetStudentCharacteristicAsync(
            [FromServices] IMediator mediator,
            CancellationToken cancellationToken)
            => await mediator.Send(
                new GetPositiveStudentCharacteristicQuery(), cancellationToken);

        [HttpGet]
        public async Task<GetLightStudentProgressesResponse> GetStudentSurveysLight(
            [FromServices] IMediator mediator,
            CancellationToken cancellationToken)
            => await mediator.Send(
                new GetStudentProgressesQuery(), cancellationToken);

        
        [HttpPost]
        public async Task<GetCurrentStudentSurveyResponse> GetCurrentStudentSurvey(
            [FromServices] IMediator mediator, GetCurrentStudentSurveyQuery request,
            CancellationToken cancellationToken)
            => await mediator.Send(request, cancellationToken);
    }
}
