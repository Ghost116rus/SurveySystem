using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveySystem.Aplication.Requests.Auth.Login;
using SurveySystem.Aplication.Requests.Auth.Register;
using SurveySystem.Requests.Auth;
using Swashbuckle.AspNetCore.Annotations;

namespace SurveySystem.Web.Controllers
{
    /// <summary>
    /// Контроллер для авторизации
    /// </summary>
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        /// <summary>
        /// Логин
        /// </summary>
        /// <param name="mediator">Медиатор CQRS</param>
        /// <param name="request">Запрос</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Объект логина</returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(AuthResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
        public async Task<ActionResult<AuthResponse>> Login(
            [FromServices] IMediator mediator,
            [FromBody] LoginRequest request,
            CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            return Ok(await mediator.Send(
                new LoginQuery
                {
                    Login = request.Login,
                    Password = request.Password,
                },
                cancellationToken));
        }

        /// <summary>
        /// Зарегистрироваться студентом
        /// </summary>
        /// <param name="mediator">Медиатор CQRS</param>
        /// <param name="request">Запрос</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Идентификатор созданного студента</returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(AuthResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
        public async Task<AuthResponse> PostRegisterAsync(
            [FromServices] IMediator mediator,
            [FromBody] RegisterStudentCommand request,
            CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            return await mediator.Send(request,
                cancellationToken);
        }
    }
}
