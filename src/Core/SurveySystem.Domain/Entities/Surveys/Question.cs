using SurveySystem.Domain.Entities.Base;
using SurveySystem.Domain.Enums;
using SurveySystem.Domain.Exceptions;


namespace SurveySystem.Domain.Entities.Surveys
{
    public class Question : EntityWTags
    {
        /// <summary>
        /// Поле для <see cref="_courseProjects"/>
        /// </summary>
        public const string SemestersField = nameof(_answers);

        /// <summary>
        /// Поле для <see cref="_users"/>
        /// </summary>
        public const string InstitutesField = nameof(_criteries);

        private List<Answer> _answers = new();
        private List<QuestionEvaluationCriteria> _criteries = new();

        public Question(QuestionType questionType, string text) 
        {
            Type = questionType;
            UpdateQuestionText(text);
        }

        protected Question() { }

        /// <summary>
        /// Тип вопроса. Подробно какие типы есть смотри в <see cref="QuestionType"/>
        /// </summary>
        public QuestionType Type { get; private set; }

        /// <summary>
        /// Текст вопроса
        /// </summary>
        public string Text { get; private set; } = "";



        #region Navigation properties

        /// <summary>
        /// Список критериев оценки, в случае открытого ответа 
        /// </summary>
        public IReadOnlyList<QuestionEvaluationCriteria>? Criteries => _criteries;

        /// <summary>
        /// Список ответов на данный вопрос
        /// </summary>
        public IReadOnlyList<Answer>? Answers => _answers;

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
