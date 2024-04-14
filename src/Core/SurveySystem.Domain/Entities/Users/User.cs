using SurveySystem.Domain.Entities.Base;
using SurveySystem.Domain.Entities.Surveys;
using SurveySystem.Domain.Enums;
using SurveySystem.Domain.Exceptions;
using SurveySystem.Domain.Extensions;

namespace SurveySystem.Domain.Entities.Users
{
    public class User : BaseEntity
    {
        /// <summary>
        /// Поле для <see cref="_student"/>
        /// </summary>
        public const string StudentField = nameof(_student);

        /// <summary>
        /// Поле для <see cref="_createdSurveys"/>
        /// </summary>
        public const string CreatedSurveysField = nameof(_createdSurveys);

        /// <summary>
        /// Поле для <see cref="_modifiedSurveys"/>
        /// </summary>
        public const string ModifiedSurveysField = nameof(_modifiedSurveys);

        private string _login;
        private string _passwordHash;
        private Student? _student;
        private string _fullName;

        private List<Survey>? _createdSurveys = new();
        private List<Survey>? _modifiedSurveys = new();

        /// <summary>
        /// Конструткор
        /// </summary>
        /// <param name="fullName">Полное имя</param>
        /// <param name="login">Логин</param>
        /// <param name="passwordHash">Хеш пароля</param>
        /// <param name="role">Роль</param>
        public User(
            string fullName,
            string login,
            string passwordHash,
            Role role = default)
        {
            FullName = fullName;
            Login = login;
            PasswordHash = passwordHash;
            Role = role;

            //if (role == Role.Student)
            //    AddDomainEvent(new StudentRegisteredDomainEvent(
            //        user: this,
            //        lastName: lastName,
            //        firstName: firstName,
            //        birthday: birthday));
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        protected User()
        {
        }

        /// <summary>
        /// Логин
        /// </summary>
        public string Login
        {
            get => _login;
            private set => _login = value
                    ?? throw new RequiredFieldNotSpecifiedException("Логин");
        }

        /// <summary>
        /// Хеш пароля
        /// </summary>
        public string PasswordHash
        {
            get => _passwordHash;
            set => _passwordHash = value
                    ?? throw new RequiredFieldNotSpecifiedException("Хеш пароля");
        }

        public string FullName 
        { 
            get => _fullName; 
            private set
                => _fullName = string.IsNullOrEmpty(value) 
                ? throw new RequiredFieldNotSpecifiedException("UserFullName") 
                : value;            
        }

        public Role Role { get; private set; }


        #region Navigation properties

        /// <summary>
        /// Ссылка на студента
        /// </summary>
        public Student? Student
        {
            get => _student;
            private set
            {
                ArgumentNullException.ThrowIfNull(value);
                if (Role != Role.Student)
                    throw new BadDataException($"Не совпадение роли и навигационного свойства ({Role.Student.GetDecription()})");
                _student = value;
            }
        }


        /// <summary>
        /// Созданные опросы
        /// </summary>
        public IReadOnlyList<Survey>? CreatedSurveys => _createdSurveys;

        /// <summary>
        /// Измененные опросы
        /// </summary>
        public IReadOnlyList<Survey>? ModifiedSurveys => _modifiedSurveys;

        #endregion

        /// <summary>
        /// Обновить информацию о пользователе
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="passwordHash">Пароль</param>
        /// <param name="fullname">Полное имя</param>
        public void UpdateInfo(
            string? login = default,
            string? passwordHash = default,
            string? fullname = default)
        {
            if (login != null && Login != login)
                Login = login;
            if (passwordHash != null && PasswordHash != passwordHash)
                PasswordHash = passwordHash;
            if (fullname != null && FullName != fullname)
                FullName = fullname;
        }

    }
}
