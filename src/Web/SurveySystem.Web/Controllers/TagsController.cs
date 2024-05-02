using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveySystem.Aplication.Requests.Tags.Create;
using SurveySystem.Aplication.Requests.Tags.Delete;
using SurveySystem.Aplication.Requests.Tags.GetAll;
using SurveySystem.Aplication.Requests.Tags.Update;
using SurveySystem.Requests.Tags;
using Swashbuckle.AspNetCore.Annotations;

namespace SurveySystem.Web.Controllers
{
    [Authorize(Roles = "SurveyMaker, Administrator")]
    public class TagsController : BaseController
    {
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GetAllTagsResponse))]
        public async Task<ActionResult<GetAllTagsResponse>> GetAll(
            [FromServices] IMediator mediator,
             CancellationToken cancellationToken)
                => Ok(await mediator.Send(
                    new GetAllTagsQuerry(), cancellationToken));



        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, type: typeof(Guid))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateTagCommand request,
            [FromServices] IMediator mediator,
            CancellationToken cancellationToken)
        {
            var id = await mediator.Send(request, cancellationToken);
            return Created(id.ToString(), id);
        }


        [HttpPut]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
        public async Task<NoContentResult> Update([FromBody] UpdateTagCommand request,
            [FromServices] IMediator mediator,
            CancellationToken cancellationToken)
        {
            await mediator.Send(request, cancellationToken);
            return NoContent();
        }


        [HttpDelete]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
        public async Task<NoContentResult> Delete([FromBody] DeleteTagCommand request,
            [FromServices] IMediator mediator,
            CancellationToken cancellationToken)
        {
            await mediator.Send(request, cancellationToken);
            return NoContent();
        }


    }
}
