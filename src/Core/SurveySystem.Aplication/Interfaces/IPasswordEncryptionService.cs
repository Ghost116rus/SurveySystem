using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Aplication.Interfaces
{
    /// <summary>
    /// Сервис хэширования паролей.
    /// </summary>
    public interface IPasswordEncryptionService
    {
        /// <summary>
        /// Хэшировать пароль.
        /// </summary>
        /// <param name="password">Пароль в чистом виде.</param>
        /// <returns>Хэш пароля.</returns>
        string EncodePassword(string password);

        /// <summary>
        /// Проверить хэш пароля на корректность.
        /// </summary>
        /// <param name="password">Пароль в чистом виде.</param>
        /// <param name="encodedPassword">Хэш пароля.</param>
        /// <returns>TRUE, если хэш пароля в чистом виде совпадает с хэшем пароля.</returns>
        bool ValidatePassword(string password, string encodedPassword);
    }
}
