using SurveySystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Aplication.Interfaces
{
    public interface IUserContext
    {
        /// <summary>
        /// ИД текущего пользователя
        /// </summary>
        Guid CurrentUserId { get; }

        /// <summary>
        /// Роль текущего пользователя
        /// </summary>
        Role CurrentUserRole { get; }
    }
}
