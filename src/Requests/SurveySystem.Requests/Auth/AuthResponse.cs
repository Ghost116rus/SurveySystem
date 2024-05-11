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
            string fullName,
            string role,
            string token)
        {
            FullName = fullName;
            Role = role;
            Token = token;
        }

        /// <summary>
        /// Полное имя пользователя
        /// </summary>
        public string FullName { get; }

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
