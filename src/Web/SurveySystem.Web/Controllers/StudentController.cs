﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveySystem.Aplication.Requests.Student.Characteristics.GetStudentCharacteristic;
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
    }
}
