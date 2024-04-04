using SurveySystem.Domain.Entities.Base;
using SurveySystem.Domain.Exceptions;

namespace SurveySystem.Domain.Entities.Surveys
{
    public class QuestionEvaluationCriteria : BaseEntity
    {
        /// <summary>
        /// Поле для <see cref="_questions"/>
        /// </summary>
        public const string QuestionsField = nameof(_questions);

        private List<Question> _questions = new();

        public QuestionEvaluationCriteria(string criteria)
        {
            Criteria = string.IsNullOrEmpty(criteria) ? throw new RequiredFieldNotSpecifiedException("criteria") : criteria;
        }

        /// <summary>
        /// конструктор для EF
        /// </summary>
        protected QuestionEvaluationCriteria() { }

        /// <summary>
        /// Текст критерия
        /// </summary>
        public string Criteria { get; set; } = "";


        #region NavigfationProperties

        public IReadOnlyCollection<Question> Questions { get => _questions; }

        #endregion

        public void UpdateQuestionsList(List<Question> questions) 
            => _questions = questions == null ? throw new ArgumentNullException("Список вопросов не должен быть null") : questions;
    }
}
