namespace SurveySystem.Domain.Exceptions
{
	/// <summary>
	/// Исключение были присланы неправильные данные
	/// </summary>
	public class BadDataException : ExceptionBase
	{
		/// <summary>
		/// Исключение "незаполнено обязательное поле"
		/// </summary>
		public BadDataException()
			: base("Неправильно заполнены данные")
		{
		}

		/// <summary>
		/// Исключение "незаполнено обязательное поле"
		/// </summary>
		/// <param name="field">Обязательное для заполнения поле</param>
		public BadDataException(string message)
			: base($"Неправильно заполнены данные: {message}")
		{
		}
	}
}
