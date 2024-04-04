using SurveySystem.Aplication.Interfaces;

namespace SurveySystem.Aplication.Services
{
    /// <summary>
    /// Провайдер даты
    /// </summary>
    public class DateTimeProvider : IDateTimeProvider
    {
        /// <inheritdoc/>
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
