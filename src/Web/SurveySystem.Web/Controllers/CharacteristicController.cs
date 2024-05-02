using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveySystem.Aplication.Requests.Characteristics.Create;
using SurveySystem.Aplication.Requests.Characteristics.Delete;
using SurveySystem.Aplication.Requests.Characteristics.GetAll;
using SurveySystem.Aplication.Requests.Characteristics.Patch;
using SurveySystem.Aplication.Requests.Characteristics.Put;
using SurveySystem.Requests.Characteristic.GetAll;
using Swashbuckle.AspNetCore.Annotations;

namespace SurveySystem.Web.Controllers
{
    [Authorize(Roles = "SurveyMaker, Administrator")]
    public class CharacteristicController : BaseController
    {
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GetAllCharacteristicResponse))]
        public async Task<ActionResult<GetAllCharacteristicResponse>> GetAll(
            [FromServices] IMediator mediator,
            CancellationToken cancellationToken)
            => Ok(await mediator.Send(
                new GetAllCharacteristicsQuery(), cancellationToken));


        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, type: typeof(Guid))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateCharacteristicCommand request,
            [FromServices] IMediator mediator,
            CancellationToken cancellationToken)
        {
            var id = await mediator.Send(request, cancellationToken);
            return Created(id.ToString(), id);
        }

        [HttpPatch]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
        public async Task<NoContentResult> Patch([FromBody] PatchCharacteristicCommand request,
            [FromServices] IMediator mediator,
            CancellationToken cancellationToken)
        {
            await mediator.Send(request, cancellationToken);
            return NoContent();
        }
           
        [HttpPut]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
        public async Task<NoContentResult> Update([FromBody] PutCharacteristicCommand request,
            [FromServices] IMediator mediator,
            CancellationToken cancellationToken)
        {
            await mediator.Send(request, cancellationToken);
            return NoContent();
        }           


        [HttpDelete]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
        [Authorize(Roles = "Administrator")]

        public async Task<NoContentResult> Delete([FromBody] DeleteCharacteristicCommand request,
            [FromServices] IMediator mediator,
            CancellationToken cancellationToken)
        {
            await mediator.Send(request, cancellationToken);
            return NoContent();
        }


    }
}
