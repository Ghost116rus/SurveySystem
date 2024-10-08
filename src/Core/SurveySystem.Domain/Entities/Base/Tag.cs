﻿using SurveySystem.Domain.Entities.Surveys;
using SurveySystem.Domain.Enums;
using SurveySystem.Domain.Exceptions;

namespace SurveySystem.Domain.Entities.Base
{
    /// <summary>
    /// Тэг используется для быстрой сортировки опросов, вопросов и ответов
    /// </summary>
    public class Tag : BaseEntity
    {
        public Tag(string description, TagType tagType)
        {
            Description = string.IsNullOrEmpty(description) ? throw new RequiredFieldNotSpecifiedException("text") : description;
            Type = tagType;
        }

        protected Tag() { }

        /// <summary>
        /// Описание тэга
        /// </summary>
        public string Description { get; private set; } = "";

        public TagType Type { get; private set; }


        #region NavigfationProperties

        public List<Survey> Surveys { get; set; } = new();

        public List<Question> Questions { get; set; } = new();

        #endregion

        public void UpdateInfo(string description, TagType type)
        {
            Description = description; 
            Type = type;
        }

    }
}
