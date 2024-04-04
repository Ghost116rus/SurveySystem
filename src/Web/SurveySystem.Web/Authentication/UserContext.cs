using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.Enums;
using System.Security.Claims;

namespace SurveySystem.Web.Authentication
{
    /// <summary>
    /// Контекст текущего пользователя
    /// </summary>
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="httpContextAccessor">Адаптер Http-context'а</param>
        public UserContext(IHttpContextAccessor httpContextAccessor)
            => _httpContextAccessor = httpContextAccessor;

        /// <inheritdoc/>
        public Guid CurrentUserId => Guid.TryParse(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId)
            ? userId
            : Guid.Empty;

        public string CurrentUserRoleName => User?.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;

        public Role CurrentUserRole => Role.Student;

        private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;
    }
}
