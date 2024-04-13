using SurveySystem.Domain.Entities.Base;
using SurveySystem.Domain.Entities.Organization;
using SurveySystem.Domain.Enums;
using SurveySystem.Domain.Exceptions;

namespace SurveySystem.Domain.Entities.Users
{
    /// <summary>
    /// Является раширением класса <see cref="Users.User"/> со связью 1 к 1
    /// </summary>
    public class Student : BaseEntity
    {
        /// <summary>
        /// Поле для <see cref="_faculty"/>
        /// </summary>
        public const string FacultyField = nameof(_faculty);

        /// <summary>
        /// Поле для <see cref="_users"/>
        /// </summary>
        public const string UserField = nameof(_user);

        /// <summary>
        /// Поле для <see cref="_characteristics"/>
        /// </summary>
        public const string SrudentCharacteristicsField = nameof(_characteristics);

        /// <summary>
        /// Поле для <see cref="_surveysProgress"/>
        /// </summary>
        public const string ProgressField = nameof(_surveysProgress);

        private DateTime _startDateOfLearning;
        private User? _user;
        private Faculty? _faculty;
        private List<StudentCharacteristic> _characteristics = new();
        private List<StudentSurveyProgress> _surveysProgress = new();

        /// <summary>
        /// Студент является расширением класса User со связью 1 к 1
        /// </summary>
        /// <param name="user">Обязательный параметр, UserId является Id студента</param>
        /// <param name="educationLevel">уровень образования</param>
        /// <param name="groupNumber">номер группы</param>
        /// <param name="faculty">кафедра</param>
        /// <param name="startDateOfLearning">Дата начала обучения</param>
        public Student(User user, EducationLevel educationLevel, string groupNumber, 
            Faculty faculty, DateTime startDateOfLearning)
        {
            Id = user.Id;
            User = user;
            StartDateOfLearning = startDateOfLearning;
            EducationLevel = educationLevel;
            GroupNumber = groupNumber;
            Faculty = faculty;
        }

        /// <summary>
        /// Конструктор для EF
        /// </summary>
        protected Student()
        {
        }


        #region Информация о получении образования

        /// <summary>
        /// Дата начала обучения
        /// </summary>
        public DateTime StartDateOfLearning 
        {
            get => _startDateOfLearning;
            private set
            {
                _startDateOfLearning = value == default
                ? throw new RequiredFieldNotSpecifiedException("Дата начала обучения")
                : value.ToUniversalTime();
            }
                
        }

        /// <summary>
        /// Уровень образования
        /// </summary>
        public EducationLevel? EducationLevel { get; private set; }

        /// <summary>
        /// Номер группы
        /// </summary>
        public string? GroupNumber { get; private set; }

        /// <summary>
        /// Идентификатор кафедры
        /// </summary>
        public Guid? FacultyId { get; private set; }

        #endregion

        #region Navigation properties

        /// <summary>
        /// Пользователь
        /// </summary>
        public User? User
        {
            get => _user;
            private set
            {
                ArgumentNullException.ThrowIfNull(value);

                if (Id != default && Id != value.Id)
                    throw new ExceptionBase("Id назначяемого пользователя не соответсвует Id студента");

                if (value.Role != Role.Student)
                    throw new ExceptionBase("Неправильная роль у студента");

                _user = value;
                Id = value.Id;
            }
        }

        /// <summary>
        /// Кафедра
        /// </summary>
        public Faculty? Faculty
        {
            get => _faculty;
            private set
            {
                _faculty = value
                    ?? throw new RequiredFieldNotSpecifiedException("Кафедра");
                FacultyId = value.Id;
            }
        }

        /// <summary>
        /// Качества студента
        /// </summary>
        public IReadOnlyList<StudentCharacteristic> StudentCharacteristics => _characteristics;

        /// <summary>
        /// Прогресс студента в прохождении тестов
        /// </summary>
        public IReadOnlyList<StudentSurveyProgress> Progresses => _surveysProgress;


        #endregion


        /// <summary>
        /// Добавить/Обновить информацию о получении образования
        /// </summary>
        /// <param name="educationLevel">Уровень образования</param>
        /// <param name="groupNumber">Номер группы</param>
        /// <param name="faculty">Кафедра</param>
        /// <param name="startDateOfLearning">Дата начала обучения</param>
        public void UpsertEducationInformation(
            EducationLevel? educationLevel = default,
            string? groupNumber = default,
            Faculty? faculty = default,
            DateTime startDateOfLearning = default)
        {
            if (educationLevel != default && EducationLevel != educationLevel)
                EducationLevel = educationLevel;
            if (groupNumber != null && GroupNumber != groupNumber)
                GroupNumber = groupNumber;
            if (startDateOfLearning != default && StartDateOfLearning != startDateOfLearning)
                StartDateOfLearning = startDateOfLearning;
            if (faculty != null && FacultyId != faculty.Id)
                Faculty = faculty;
        }



    }
}
