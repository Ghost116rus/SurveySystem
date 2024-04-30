using Microsoft.EntityFrameworkCore;
using SurveySystem.Domain.Entities.Base;
using SurveySystem.Domain.Entities.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Aplication.Extenstions
{
    /// <summary>
    /// Расишрение для фильтрации
    /// </summary>
    public static class Filters
    {
        private static IQueryable<EntityWTags> FilterEntityWTags(this IQueryable<EntityWTags> query, IEnumerable<Guid> tags)
        {
            return query = query
                    .Include(x => x.Tags)
                    .Where(x => x.Tags!.Any(t => tags.Contains(t.Id)));
        }


        /// <summary>
        /// Создать фильтр для участий в мероприятии
        /// </summary>
        /// <param name="query">Запрос</param>
        /// <param name="userContext">Контекст текущего пользователя</param>
        /// <returns>Запрос с фильтром</returns>
        public static IQueryable<Survey> FilterByTags(this IQueryable<Survey> query, IEnumerable<Guid> tags)
        {
            ArgumentNullException.ThrowIfNull(query);

            if (tags != null && tags.Count() >= 1)
                query.FilterEntityWTags(tags);

            return query;
        }
    }
}
