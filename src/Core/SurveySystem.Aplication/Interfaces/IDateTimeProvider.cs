namespace SurveySystem.Aplication.Interfaces
{
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Текущее время
        /// необходим для Unit-тестов
        /// </summary>
        DateTime UtcNow { get; }
    }
}
