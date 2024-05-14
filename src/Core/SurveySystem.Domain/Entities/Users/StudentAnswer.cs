using SurveySystem.Domain.Entities.Base;
using SurveySystem.Domain.Entities.Surveys;

namespace SurveySystem.Domain.Entities.Users
{
    public class StudentAnswer : BaseEntity
    {
        /// <summary>
        /// Поле для <see cref="_characteristic"/>
        /// </summary>
        public const string SurveyProgressField = nameof(_surveyProgress);        
        
        /// <summary>
        /// Поле для <see cref="_characteristic"/>
        /// </summary>
        public const string AnswerField = nameof(_answer);

        private StudentSurveyProgress _surveyProgress;
        private Answer _answer;

        public StudentAnswer(StudentSurveyProgress survey, Answer answer)
        {
            SurveyProgress = survey;
            Answer = answer;
            IsActual = true;
        }

        protected StudentAnswer() { }

        public Guid SurveyProgressId { get; private set; }
        public Guid AnswerId { get; private set; }

        /// <summary>
        /// Проверяет, актуален ли ответ при составлении цифрового профиля
        /// </summary>
        public bool IsActual { get; set; }

        #region NavigfationProperties

        public StudentSurveyProgress SurveyProgress
        {
            get => _surveyProgress;
            private set
            {
                ArgumentNullException.ThrowIfNull(value);
                _surveyProgress = value;
                SurveyProgressId = value.Id;
            }
        }

        public Answer Answer
        {
            get => _answer;
            private set
            {
                ArgumentNullException.ThrowIfNull(value);
                _answer = value;
                AnswerId = value.Id;
            }
        }
        #endregion
    }
}
