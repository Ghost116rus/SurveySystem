using SurveySystem.Domain.Entities.Base;
using SurveySystem.Domain.Enums;
using SurveySystem.Domain.Exceptions;

namespace SurveySystem.Domain.Entities.Surveys
{
    public class Question : EntityWTags
    {
        /// <summary>
        /// Поле для <see cref="_answers"/>
        /// </summary>
        public const string AnswersField = nameof(_answers);

        /// <summary>
        /// Поле для <see cref="_criteries"/>
        /// </summary>
        public const string CriteriesField = nameof(_criteries);

        private List<Answer> _answers;
        private List<QuestionEvaluationCriteria> _criteries;
        private int _maxCountOfAnswers;

        public Question(QuestionType questionType, string text, int maxCountOfAnswers, List<Tag> tags, bool autoCreatedAnswers = false)
            : base(tags)
        {
            Type = questionType;
            UpdateQuestionText(text);
            _answers = new();
            _criteries = new();
            MaxCountOfAnswers = maxCountOfAnswers;
            IsAutoCreatedAnswers = autoCreatedAnswers;
        }

        protected Question() { }

        /// <summary>
        /// Тип вопроса. Подробно какие типы есть смотри в <see cref="QuestionType"/>
        /// </summary>
        public QuestionType Type { get; private set; }

        /// <summary>
        /// Текст вопроса
        /// </summary>
        public string Text { get; private set; } = default!;

        public int MaxCountOfAnswers
        {
            get => _maxCountOfAnswers;
            private set
            {
                if (value != 1 && Type != QuestionType.NonAlternative)
                    throw new BadDataException("Максимальное количество ответов можно задать " +
                        "только для вопросов с несколькими вариантами ответа");
                if (value < 1)
                    throw new BadDataException("Максимальное количество ответов не может быть меньше 1");

                _maxCountOfAnswers = value;
            }
        }

        public bool IsAutoCreatedAnswers { get; private set; }

        #region Navigation properties

        /// <summary>
        /// Список ответов на данный вопрос
        /// </summary>
        public IReadOnlyList<Answer>? Answers => _answers;

        /// <summary>
        /// Список критериев оценки, в случае открытого ответа 
        /// </summary>
        public IReadOnlyList<QuestionEvaluationCriteria>? Criteries => _criteries;

        #endregion


        /// <summary>
        /// Метод обновления текста вопроса
        /// </summary>
        /// <param name="text">новый текст вопроса</param>
        /// <exception cref="RequiredFieldNotSpecifiedException"></exception>
        public void UpdateQuestionText(string text)
            => Text = string.IsNullOrEmpty(text) ? throw new RequiredFieldNotSpecifiedException("text") : text;
    
    }
}
