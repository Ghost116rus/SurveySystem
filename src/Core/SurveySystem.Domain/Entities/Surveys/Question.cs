using SurveySystem.Domain.Entities.Base;
using SurveySystem.Domain.Enums;
using SurveySystem.Domain.Exceptions;


namespace SurveySystem.Domain.Entities.Surveys
{
    public class Question : HasTagsEntity
    {
        public Question(QuestionType questionType, string text) 
        {
            Type = questionType;
            UpdateQuestionText(text);
        }

        protected Question() { }

        public QuestionType Type { get; private set; }

        public string Text { get; private set; } = "";

        /// <summary>
        /// Метод обновления текста вопроса
        /// </summary>
        /// <param name="text">новый текст вопроса</param>
        /// <exception cref="RequiredFieldNotSpecifiedException"></exception>
        public void UpdateQuestionText(string text)
            => Text = string.IsNullOrEmpty(text) ? throw new RequiredFieldNotSpecifiedException("text") : text;
    }
}
