﻿using SurveySystem.Domain.Entities.Base;
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

        public Question(QuestionType questionType, string text) 
        {
            Type = questionType;
            UpdateQuestionText(text);
            _answers = new();
            _criteries = new();
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
