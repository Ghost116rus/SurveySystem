using SurveySystem.Domain.Entities.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Domain.Entities.Base
{
    public abstract class HasTagsEntity : BaseEntity
    {
        /// <summary>
        /// Поле для <see cref="_faculty"/>
        /// </summary>
        public const string TagsField = nameof(_tags);

        protected List<Tag> _tags;

        public HasTagsEntity(List<Tag> tags)
            => _tags = tags;
        protected HasTagsEntity()
        {           
        }

        #region Navigation properties

        /// Список тега для простоты поиска опроса
        /// </summary>
        public IReadOnlyList<Tag>? Tags => _tags;

        #endregion
    }
}
