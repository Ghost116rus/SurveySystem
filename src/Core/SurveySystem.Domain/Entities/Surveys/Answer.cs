using SurveySystem.Domain.Entities.Base;
using SurveySystem.Domain.Entities.Users;
using SurveySystem.Domain.Exceptions;

namespace SurveySystem.Domain.Entities.Surveys
{
    public class Answer : BaseEntity
    {
        /// <summary>
        /// Поле для <see cref="_question"/>
        /// </summary>
        public const string QuestionField = nameof(_question);

        /// <summary>
        /// Поле для <see cref="_answersCharaterisics"/>
        /// </summary>
        public const string AnswerCharacteristicField = nameof(_answersCharaterisics);

        /// <summary>
        /// Поле для <see cref="_studentAnswers"/>
        /// </summary>
        public const string StudentAnswersField = nameof(_studentAnswers);

        private Question? _question;
        private List<AnswerCharacteristicValue> _answersCharaterisics = new();
        private List<StudentAnswer> _studentAnswers = new();

        public Answer(string text, Question question)
        {
            Question = question;
            UpdateQuestionText(text);
        }

        protected Answer() { }

        public Guid QuestionId { get; private set; }

        /// <summary>
        /// Текст вопроса
        /// </summary>
        public string Text { get; private set; } = "";



        #region NavigfationProperties

        public Question? Question
        {
            get => _question;
            private set
            {
                ArgumentNullException.ThrowIfNull(value);
                _question = value;
                QuestionId = value.Id;
            }
        }
        public IReadOnlyCollection<AnswerCharacteristicValue> AnswerCharacteristicValues { get => _answersCharaterisics; }
        public IReadOnlyCollection<StudentAnswer> StudentAnswers { get => _studentAnswers; }

        #endregion

        /// <summary>
        /// Метод обновления текста ответа
        /// </summary>
        /// <param name="text">новый текст вопроса</param>
        /// <exception cref="RequiredFieldNotSpecifiedException"></exception>
        public void UpdateQuestionText(string text)
            => Text = string.IsNullOrEmpty(text) ? throw new RequiredFieldNotSpecifiedException("text") : text;

    }
}
