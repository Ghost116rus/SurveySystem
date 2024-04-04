using SurveySystem.Domain.Exceptions;

namespace SurveySystem.Domain.Entities.Surveys
{
    public class SurveyTestQuestion
    {
        /// <summary>
        /// Поле для <see cref="_survey"/>
        /// </summary>
        public const string SurveyField = nameof(_survey);

        /// <summary>
        /// Поле для <see cref="_question"/>
        /// </summary>
        public const string QuestionField = nameof(_question);

        private int _position;
        private Survey? _survey;
        private Question? _question;

        public SurveyTestQuestion(int postion, Survey survey, Question question)
        {            
            Survey = survey;
            Question = question;
            Position = postion;
        }

        protected SurveyTestQuestion() { }

        public Guid SurveyId { get; private set; }
        public Guid QuestionId { get; private set; }

        /// <summary>
        /// Степень выраженности данной особенности пользователя
        /// </summary>
        public int Position
        {
            get => _position;
            set
            {
                if (value < 1)
                    throw new ExceptionBase("Позиция не может быть меньше 1");
                else
                    _position = value;
            }
        }

        #region NavigfationProperties

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

        #endregion
    }
}
