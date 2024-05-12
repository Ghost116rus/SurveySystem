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
                if (value < 0)
                    throw new BadDataException("Позиция не может быть меньше 0");
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
                if (value is null)
                    throw new ExceptionBase("Без существующей анкеты невозможно создать тестовый вопрос");

                _survey = value;
                SurveyId = value.Id;
            }
        }

        public Question? Question
        {
            get => _question;
            private set
            {
                if (value is null)
                    throw new ExceptionBase("Невозможно создать тестовый вопрос без вопроса");
                _question = value;
                QuestionId = value.Id;
            }
        }

        #endregion
    }
}
