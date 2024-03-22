using SurveySystem.Domain.Constants;
using SurveySystem.Domain.Entities.Base;
using SurveySystem.Domain.Entities.Organization;
using SurveySystem.Domain.Exceptions;

namespace SurveySystem.Domain.Entities.Surveys
{
    public class Survey : EntityWTags
    {
        /// <summary>
        /// Поле для <see cref="_courseProjects"/>
        /// </summary>
        public const string SemestersField = nameof(_semesters);

        /// <summary>
        /// Поле для <see cref="_users"/>
        /// </summary>
        public const string InstitutesField = nameof(_institutes);

        /// <summary>
        /// Поле для <see cref="_faculty"/>
        /// </summary>
        public const string FacultiesField = nameof(_faculties);

        /// <summary>
        /// Поле для <see cref="_questions"/>
        /// </summary>
        public const string QuestionsField = nameof(_questions);

        private DateTime? _startDate;
        private List<int> _semesters = new List<int>();
        private List<Institute> _institutes = new List<Institute>();
        private List<Faculty> _faculties = new List<Faculty>();
        private List<SurveyTestQuestion> _questions = new List<SurveyTestQuestion>();


        public Survey(string name, DateTime? startDate, bool isRepetable, bool isVisible,
            List<Institute> institutes, List<Faculty> faculties, List<int> semesters, List<Tag> tags, List<SurveyTestQuestion> surveyTestQuestions) : base(tags)
        {
            Name = name;
            IsRepetable = isRepetable;
            IsVisible = isVisible;
            StartDate = startDate;
            _institutes = institutes ?? _institutes;
            _tags = tags;
            UpdateSurveyQuestions(surveyTestQuestions);
            SetFaculties(faculties);
            SetSemesters(semesters);
        }

        /// <summary>
        /// Конструктор для EF
        /// </summary>
        protected Survey()
        {
        }

        /// <summary>
        /// Название опроса
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Определяет время начала опроса - в случае null - не используется
        /// </summary>
        public DateTime? StartDate 
        {
            get => _startDate;
            private set
            {
                _startDate = value?.ToUniversalTime();
            }
        }

        /// <summary>
        /// Определяет возможность пройти опрос заново
        /// </summary>
        public bool IsRepetable { get; private set; } 

        /// <summary>
        /// Определяет видимость опроса
        /// </summary>
        public bool IsVisible { get; private set; }



        #region Navigation properties

        /// <summary>
        /// Список назначенных институтов для назначения опроса - в случае, 
        /// если заданный список пустой, назначается всем студентам, что подходят под ограничение семестра, уровня образования и даты
        /// </summary>
        public IReadOnlyList<int>? Semester => _semesters;

        /// <summary>
        /// Список назначенных институтов для назначения опроса - в случае, 
        /// если заданный список пустой, назначается всем студентам, что подходят под ограничение семестра, уровня образования и даты
        /// </summary>
        public IReadOnlyList<Institute>? Institutes => _institutes;

        /// <summary>
        /// Список назначенных кафедр для прохождения опроса - в случае, если список пустой,
        /// назначается всем группам, что подходят под ограничение семестра и даты
        /// </summary>
        public IReadOnlyList<Faculty>? Faculties => _faculties;

        // TODO: добавить группы

        /// <summary>
        /// Список вопросов
        /// </summary>
        public IReadOnlyList<SurveyTestQuestion>? Questions => _questions;

        #endregion

        private void SetFaculties(List<Faculty> faculties)
        {
            if (_institutes == null)
                throw new ExceptionBase("Невозможно назначить кафедру, без выбора института");
            ArgumentNullException.ThrowIfNull(faculties);
            _faculties = faculties;
        }

        private void SetSemesters(List<int> semesters)
        {
            foreach (var value in semesters)
            {
                if (value <= 0)
                    throw new ExceptionBase($"Семестр не может быть меньше или равен нулю");
                else if (value > DomainConstants.SemesterConstant)
                    throw new ExceptionBase($"Семестр не может быть больше заданного в программе числа ({DomainConstants.SemesterConstant})");
                _semesters.Add(value);
            }
        }

        public void UpdateSurveyQuestions(List<SurveyTestQuestion> surveyTestQuestions)
        {
            if (surveyTestQuestions is null)
                throw new RequiredFieldNotSpecifiedException("Список вопросов анкеты не должен быть null");
            _questions = surveyTestQuestions;
        }
    }
}
