using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Domain.Exceptions
{
    public class AuthentificateException : ExceptionBase
    {
        /// <inheritdoc/>
        public AuthentificateException(string message)
            : base(message)
        {
        }
    }
}
