using SurveySystem.Domain.Entities.Base;
using SurveySystem.Domain.Entities.Organization;
using SurveySystem.Domain.Entities.Users;
using SurveySystem.Domain.Exceptions;
using SurveySystem.Domain.Interfaces;

namespace SurveySystem.Domain.Entities.Surveys
{
    public class Survey : EntityWTags, IUserTrackable
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

        /// <summary>
        /// Поле для <see cref="_createdByUser"/>
        /// </summary>
        public const string CreatedByUserField = nameof(_createdByUser);

        /// <summary>
        /// Поле для <see cref="_modifiedByUser"/>
        /// </summary>
        public const string ModifiedByUserField = nameof(_modifiedByUser);

        private User? _createdByUser;
        private User? _modifiedByUser;

        private DateTime? _startDate;
        private List<Semester> _semesters = new List<Semester>();
        private List<Institute> _institutes = new List<Institute>();
        private List<Faculty> _faculties = new List<Faculty>();
        private List<SurveyTestQuestion> _questions = new List<SurveyTestQuestion>();


        public Survey(string name, DateTime? startDate, bool isRepetable, bool isVisible,
            List<Institute> institutes, List<Faculty> faculties, List<Semester> semesters, List<Tag> tags, List<SurveyTestQuestion> surveyTestQuestions) : base(tags)
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

        /// <summary>
        /// Идентификатор пользователя, создавшего сущность
        /// </summary>
        public Guid CreatedByUserId { get; set; }

        /// <summary>
        /// Идентификатор пользователя, изменившего сущность
        /// </summary>
        public Guid ModifiedByUserId { get; set; }

        #region Navigation properties

        /// <summary>
        /// Список назначенных институтов для назначения опроса - в случае, 
        /// если заданный список пустой, назначается всем студентам, что подходят под ограничение семестра, уровня образования и даты
        /// </summary>
        public IReadOnlyList<Semester>? Semesters => _semesters;

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

        /// <summary>
        /// Пользователь, создавший сущность
        /// </summary>
        public User? CreatedByUser
        {
            get => _createdByUser;
            set
            {
                _createdByUser = value
                    ?? throw new RequiredFieldNotSpecifiedException("Пользователь, создавший сущность");
                CreatedByUserId = value.Id;
            }
        }

        /// <summary>
        /// Пользователь, изменивший сущность
        /// </summary>
        public User? ModifiedByUser
        {
            get => _modifiedByUser;
            set
            {
                _modifiedByUser = value
                    ?? throw new RequiredFieldNotSpecifiedException("Пользователь, изменивший сущность");
                ModifiedByUserId = value.Id;
            }
        }

        #endregion

        private void SetFaculties(List<Faculty> faculties)
        {
            if (_institutes == null)
                throw new BadDataException("Невозможно назначить кафедру, без выбора института");
            ArgumentNullException.ThrowIfNull(faculties);
            _faculties = faculties;
        }

        private void SetSemesters(List<Semester> semesters) => _semesters = semesters;
        

        public void UpdateSurveyQuestions(List<SurveyTestQuestion> surveyTestQuestions)
        {
            if (surveyTestQuestions is null || surveyTestQuestions.Count < 1)
                throw new RequiredFieldNotSpecifiedException("Для создания опроса необходим хотя бы 1 вопрос");
            _questions = surveyTestQuestions;
        }
    }
}
