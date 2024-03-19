using SurveySystem.Domain.Entities.Base;
using SurveySystem.Domain.Exceptions;

namespace SurveySystem.Domain.Entities.Surveys
{
    /// <summary>
    /// Тэг используется для быстрой сортировки опросов, вопросов и ответов
    /// </summary>
    public class Tag : BaseEntity
    {
        /// <summary>
        /// Описание тэга
        /// </summary>
        public string Description { get; private set; } = "";
        public Tag(string description)
            => Description = string.IsNullOrEmpty(description) ? throw new RequiredFieldNotSpecifiedException("text") : description;

        protected Tag() { }
    }
}
