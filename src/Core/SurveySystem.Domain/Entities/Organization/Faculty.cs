using SurveySystem.Domain.Entities.Base;
using SurveySystem.Domain.Entities.Surveys;
using SurveySystem.Domain.Entities.Users;
using SurveySystem.Domain.Exceptions;

namespace SurveySystem.Domain.Entities.Organization
{
    public class Faculty : BaseEntity
    {
        /// <summary>
        /// Поле для <see cref="_institute"/>
        /// </summary>
        public const string InstituteField = nameof(_institute);

        /// <summary>
        /// Поле для <see cref="_students"/>
        /// </summary>
        public const string StudentsField = nameof(_students);

        /// <summary>
        /// Поле для <see cref="_surveys"/>
        /// </summary>
        public const string SurveysField = nameof(_surveys);

        private string _fullName;
        private string _shortName;

        private Institute _institute;

        private List<Student> _students; 
        private List<Survey>? _surveys;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="fullName">Полное имя</param>
        /// <param name="shortName">Сокращенное имя</param>
        public Faculty(
            string fullName,
            string shortName,
            Institute institute)
        {
            FullName = fullName;
            ShortName = shortName;
            Institute = institute;
            _surveys = new List<Survey>();
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        protected Faculty()
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

        /// <summary>
        /// Идентификатор института
        /// </summary>
        public Guid InstituteId { get; private set; }

        #region Navigation properties

        /// <summary>
        /// Институт
        /// </summary>
        public Institute? Institute
        {
            get => _institute;
            private set
            {
                _institute = value
                    ?? throw new RequiredFieldNotSpecifiedException("Институт");
                InstituteId = value.Id;
            }
        }

        /// <summary>
        /// Студенты, учащиеся на кафедре
        /// </summary>
        public IReadOnlyList<Student>? Students => _students;
        public IReadOnlyList<Survey>? Surveys => _surveys;
        #endregion

    }
}
