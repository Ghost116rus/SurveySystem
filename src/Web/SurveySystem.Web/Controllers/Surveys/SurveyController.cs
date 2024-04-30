﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveySystem.Aplication.Requests.Characteristics.Create;
using SurveySystem.Aplication.Requests.Surveys.Survey.Create;
using SurveySystem.Requests.Surveys.Survey;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics.CodeAnalysis;

namespace SurveySystem.Web.Controllers.Surveys
{
    [Authorize(Roles = "SurveyMaker, Administrator")]
    public class SurveyController : BaseController
    {


        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, type: typeof(Guid))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateSurveyCommand surveyRequest,
            [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            var id = await mediator.Send(surveyRequest, cancellationToken);
            return Created(id.ToString(), id);
        }
    }
}
