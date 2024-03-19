using System.Runtime.Serialization;

namespace SurveySystem.Domain.Exceptions
{
	/// <summary>
	/// Базовое исключение для логики приложения
	/// </summary>
	public class ExceptionBase : ApplicationException
	{
		/// <summary>
		/// Базовое исключение для логики приложения
		/// </summary>
		public ExceptionBase()
		{
		}

		/// <summary>
		/// Базовое исключение для логики приложения
		/// </summary>
		/// <param name="message">Сообщение</param>
		public ExceptionBase(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Базовое исключение для логики приложения
		/// </summary>
		/// <param name="info">info</param>
		/// <param name="context">context</param>
		public ExceptionBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>
		/// Базовое исключение для логики приложения
		/// </summary>
		/// <param name="message">Сообщение</param>
		/// <param name="innerException">Внутреннее исключение</param>
		public ExceptionBase(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
