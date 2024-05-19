using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveySystem.Aplication.Requests.DigitalProfile.GetStudentInterests;
using SurveySystem.Aplication.Requests.DigitalProfile.GetStudentStudentDogitalProfile;
using SurveySystem.Requests.DigitalProfile;
using Swashbuckle.AspNetCore.Annotations;

namespace SurveySystem.Web.Controllers
{
    [Authorize(Roles = "Student")]
    public class DigitalProfileController : BaseController
    {
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GetDigitalProfileResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, type: typeof(ProblemDetails))]
        public async Task<ActionResult<GetDigitalProfileResponse>> GetStudentProfile(
            [FromServices] IMediator mediator,
            CancellationToken cancellationToken)
            => await mediator.Send(
                new GetOnlyPositiveCharacteristicInStudentProfileQuery(), cancellationToken);

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GetStudentInterestsResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, type: typeof(ProblemDetails))]
        public async Task<ActionResult<GetStudentInterestsResponse>> GetStudentInterests(
            [FromServices] IMediator mediator,
            CancellationToken cancellationToken)
            => await mediator.Send(
                new GetStudentInterestsQuery(), cancellationToken);

    }
}
