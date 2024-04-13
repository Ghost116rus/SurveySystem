namespace SurveySystem.Requests.Auth
{
    /// <summary>
    /// Ответ на команду <see cref="LoginRequest"/>
    /// </summary>
    public class AuthResponse
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <param name="token">Токен авторизации</param>
        public AuthResponse(
            Guid userId,
            string role,
            string token)
        {
            UserId = userId;
            Role = role;
            Token = token;
        }

        /// <summary>
        /// Id пользователя
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        public string Role { get; } = default!;

        /// <summary>
        /// Токен авторизации
        /// </summary>
        public string Token { get; } = default!;
    }
}
