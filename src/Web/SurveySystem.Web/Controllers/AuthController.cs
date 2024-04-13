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
        [HttpPost("Login")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(AuthResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
        public async Task<AuthResponse> LoginAsync(
            [FromServices] IMediator mediator,
            [FromBody] LoginRequest request,
            CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            return await mediator.Send(
                new LoginQuery
                {
                    Login = request.Login,
                    Password = request.Password,
                },
                cancellationToken);
        }

        /// <summary>
        /// Зарегистрироваться студентом
        /// </summary>
        /// <param name="mediator">Медиатор CQRS</param>
        /// <param name="request">Запрос</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Идентификатор созданного студента</returns>
        [HttpPost("Register/Student")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(AuthResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ProblemDetails))]
        public async Task<AuthResponse> PostRegisterAsync(
            [FromServices] IMediator mediator,
            [FromBody] RegisterStudentRequest request,
            CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            //return await mediator.Send(
            //    new RegisterStudentCommand
            //    {
            //        LastName = request.LastName,
            //        FirstName = request.FirstName,
            //        Birthday = request.Birthday,
            //        Login = request.Login,
            //        Email = request.Email,
            //        Password = request.Password,
            //        Phone = request.Phone,
            //    },
            //    cancellationToken);

            return new AuthResponse(Guid.Empty, "aw", "aw");
        }
    }
}
