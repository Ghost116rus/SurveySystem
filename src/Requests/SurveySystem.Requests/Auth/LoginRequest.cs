using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Requests.Auth
{
    /// <summary>
    /// Запрос на логин
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; } = default!;

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; } = default!;
    }
}
