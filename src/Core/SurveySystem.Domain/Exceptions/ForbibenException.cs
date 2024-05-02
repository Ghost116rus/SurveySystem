namespace SurveySystem.Domain.Exceptions
{
    public class ForbibenException : ExceptionBase
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ForbibenException()
        {
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        public ForbibenException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="innerException">Внутреннее исключение</param>
        public ForbibenException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
