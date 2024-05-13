using SurveySystem.Domain.Entities.Base;
using SurveySystem.Domain.Entities.Surveys;
using SurveySystem.Domain.Exceptions;

namespace SurveySystem.Domain.Entities.Users
{
    public class StudentSurveyProgress : BaseEntity
    {
        /// <summary>
        /// Поле для <see cref="_student"/>
        /// </summary>
        public const string StudentField = nameof(_student);

        /// <summary>
        /// Поле для <see cref="_survey"/>
        /// </summary>
        public const string SurveyField = nameof(_survey);

        /// <summary>
        /// Поле для <see cref="_studentAnswers"/>
        /// </summary>
        public const string StudentAnswersField = nameof(_studentAnswers);

        private int _currentPostion;
        private Student? _student;
        private Survey? _survey;
        private List<StudentAnswer> _studentAnswers = new();

        public StudentSurveyProgress(Student student, Survey survey)
        {
            Student = student;
            Survey = survey;
            CurrentPostion = 0;
        }
        protected StudentSurveyProgress() { }

        public int CurrentPostion
        {
            get => _currentPostion;
            private set
            {
                if (value < 0)
                    throw new BadDataException("Позиция не может быть меньше 0");
                _currentPostion = value;
            }
        }

        public bool IsCompleted { get; private set; }

        public Guid StudentId { get; private set; }
        public Guid SurveyId { get; private set; }


        #region NavigfationProperties

        public Student? Student
        {
            get => _student;
            private set
            {
                ArgumentNullException.ThrowIfNull(value);
                _student = value;
                StudentId = value.Id;
            }
        }

        public Survey? Survey
        {
            get => _survey;
            private set
            {
                ArgumentNullException.ThrowIfNull(value);
                _survey = value;
                SurveyId = value.Id;
            }
        }

        /// <summary>
        /// Ответы студента
        /// </summary>
        public IReadOnlyList<StudentAnswer>? Answers => _studentAnswers;        
        
        #endregion

        /// <summary>
        /// Метод обновления статуса завершенности теста
        /// </summary>
        /// <param name="questionsCount">Количество вопросов в анкете</param>
        public void UpdateIsCompletedStatus(int questionsCount)
            => IsCompleted = _currentPostion == questionsCount;  
        

        public void UpdatePosition(int newPosition, int countOfQuestions)
        {
            CurrentPostion = newPosition;
            IsCompleted = CurrentPostion == countOfQuestions;
        }
    }
}

