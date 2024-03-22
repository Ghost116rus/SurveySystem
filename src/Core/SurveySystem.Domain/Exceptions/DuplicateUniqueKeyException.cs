using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Domain.Exceptions
{
    /// <summary>
    /// Нарушенное ограничение уникальности в таблице БД
    /// </summary>
    public class DuplicateUniqueKeyException : ExceptionBase
    {
        public DuplicateUniqueKeyException(
            Exception innerException,
            string message = "Нарушено ограничение уникальности при обновлении базы данных. Попробуйте повторить запрос.")
            : base(message, innerException)
        {
        }
    }
}
