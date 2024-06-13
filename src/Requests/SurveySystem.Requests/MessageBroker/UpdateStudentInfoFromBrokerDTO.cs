using SurveySystem.Domain.Enums;

namespace SurveySystem.Requests.MessageBroker
{
    /// <summary>
    /// Запрос на дополнение информации об обычном студенте <see cref="User"/>, <see cref="Student"/>
    /// </summary>
    public class UpdateStudentInfoFromBrokerDTO
    {
        /// <summary>
        /// Уровень образования см. подробнее в <see cref="Domain.Enums.EducationLevel"/>
        /// </summary>
        public EducationLevel? EducationLevel { get; set; }

        public string GroupNumber { get; set; } = default!;

        /// <summary>
        /// Идентификатор кафедры
        /// </summary>
        public Guid FacultyId { get; set; }

        /// <summary>
        /// Дата начала обучения
        /// </summary>
        public DateTime StartDateOfLearning { get; set; }
    }
}
