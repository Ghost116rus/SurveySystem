using SurveySystem.Domain.Enums;

namespace SurveySystem.Requests.MessageBroker
{
    /// <summary>
    /// Запрос на обычного пользователя-студента <see cref="User"/>, <see cref="Student"/>
    /// </summary>
    public class RegisterNewStudentFromBrokerDTO
    {
        /// <summary>
        /// Guid пользователя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; } = default!;

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; } = default!;

        /// <summary>
        /// Фамилия
        /// </summary>
        public string FullName { get; set; } = default!;

    }
}
