using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SurveySystem.Domain.Entities.Base;
using SurveySystem.Domain.Exceptions;

namespace SurveySystem.Domain.Entities.Organization
{
    public class Institute : BaseEntity
    {
        /// <summary>
        /// Поле для <see cref="_faculties"/>
        /// </summary>
        public const string FacultiesField = nameof(_faculties);

        private string _fullName;
        private string _shortName;
        private List<Faculty>? _faculties;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="fullName">Полное имя</param>
        /// <param name="shortName">Сокращенное имя</param>
        public Institute(
            string fullName,
            string shortName)
        {
            FullName = fullName;
            ShortName = shortName;

            _faculties = new List<Faculty>();
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        protected Institute()
        {
        }

        /// <summary>
        /// Полное имя
        /// </summary>
        public string FullName
        {
            get => _fullName;
            private set => _fullName = string.IsNullOrWhiteSpace(value)
                ? throw new RequiredFieldNotSpecifiedException("Полное имя")
                : value;
        }

        /// <summary>
        /// Сокращенное имя
        /// </summary>
        public string ShortName
        {
            get => _shortName;
            private set => _shortName = string.IsNullOrWhiteSpace(value)
                ? throw new RequiredFieldNotSpecifiedException("Сокращенное имя")
                : value;
        }

        #region Navigation properties

        /// <summary>
        /// Кафедры
        /// </summary>
        public IReadOnlyList<Faculty>? Faculties => _faculties;

        #endregion

    }
}
