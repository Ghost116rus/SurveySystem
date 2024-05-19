using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveySystem.Aplication.Requests.Student.AnswerInSurvey;
using SurveySystem.Aplication.Requests.Student.GetCurrentStudentSurvey;
using SurveySystem.Aplication.Requests.Student.RestartSurvey;
using SurveySystem.Aplication.Requests.Student.StudentProgress;
using SurveySystem.Requests.Students;
using SurveySystem.Requests.Students.AnswerInSurvey;
using SurveySystem.Requests.Students.GetCurrentStudentSurvey;
using SurveySystem.Requests.Students.RestartSruventSurvey;
using Swashbuckle.AspNetCore.Annotations;

namespace SurveySystem.Web.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentController : BaseController
    {
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GetLightStudentProgressesResponse))]
        public async Task<ActionResult<GetLightStudentProgressesResponse>> GetStudentSurveysLight(
            [FromServices] IMediator mediator,
            CancellationToken cancellationToken)
            => await mediator.Send(
                new GetLightStudentProgressesQuery(), cancellationToken);
                
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GetCurrentStudentSurveyResponse))]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GetCurrentStudentSurveyResponse))]
        [SwaggerResponse(StatusCodes.Status403Forbidden, type: typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, type: typeof(ProblemDetails))]
        public async Task<ActionResult<GetCurrentStudentSurveyResponse>> GetCurrentStudentSurvey(
            [FromServices] IMediator mediator,
            [FromQuery] Guid studentSurveyId,
            CancellationToken cancellationToken)
            => await mediator.Send(new GetCurrentStudentSurveyQuery() { StudentSurveyId = studentSurveyId }, cancellationToken);
                
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GetCurrentStudentSurveyResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status403Forbidden, type: typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, type: typeof(ProblemDetails))]
        public async Task<ActionResult<AddNewAnswerForStudentSurveyResponse>> AddNewAnswerForStudentSurvey(
            [FromServices] IMediator mediator,
            [FromBody] AddNewAnswerForStudentSurveyCommand request,
            CancellationToken cancellationToken)
            => await mediator.Send(request, cancellationToken);
        

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GetCurrentStudentSurveyResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status403Forbidden, type: typeof(ProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, type: typeof(ProblemDetails))]
        public async Task<ActionResult<RestartSruventSurveyResponse>> RestartSruventSurvey(
            [FromServices] IMediator mediator, 
            [FromBody] RestartSurveyCommand request,
            CancellationToken cancellationToken)
            => await mediator.Send(request, cancellationToken);
    }
}
