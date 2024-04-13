using SurveySystem.Domain.Entities.Users;
using SurveySystem.Domain.Enums;

namespace SurveySystem.Requests.Auth
{
    /// <summary>
    /// Запрос на обычного пользователя-студента <see cref="User"/>, <see cref="Student"/>
    /// </summary>
    public class RegisterStudentRequest
    {
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

        /// <summary>
        /// Уровень образования см. подробнее в <see cref="Domain.Enums.EducationLevel"/>
        /// </summary>
        public EducationLevel EducationLevel { get; set; }

        public string GroupNumber { get; set; } = default!;

        /// <summary>
        /// Идентификатор кафедры
        /// </summary>
        public Guid? FacultyId { get; set; }

        /// <summary>
        /// Дата начала обучения
        /// </summary>
        public DateTime StartDateOfLearning { get; set; }
    }
}
