namespace SurveySystem.Domain.Enums
{
    /// <summary>
    /// Типы токенов
    /// </summary>
    public enum TokenTypes
    {
        /// <summary>
        /// Токен авторизации
        /// </summary>
        Auth = 1,

        /// <summary>
        /// Токен обновления
        /// </summary>
        RefreshToken = 2,
    }
}
