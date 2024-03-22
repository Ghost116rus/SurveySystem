namespace SurveySystem.Domain.Entities.Base
{
    public abstract class EntityWTags : BaseEntity
    {
        /// <summary>
        /// Поле для <see cref="_tags"/>
        /// </summary>
        public const string TagsField = nameof(_tags);

        protected List<Tag> _tags = new();

        public EntityWTags(List<Tag> tags)
            => _tags = tags;

        protected EntityWTags()
        {           
        }

        #region Navigation properties

        /// Список тега для простоты поиска опроса
        /// </summary>
        public IReadOnlyList<Tag>? Tags => _tags;

        #endregion

        /// <summary>
        /// Смена тегов
        /// </summary>
        /// <param name="tags"></param>
        public void ChangeTags(List<Tag> tags) => _tags = tags;
        
    }
}
