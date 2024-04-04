using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Aplication.Services;
using SurveySystem.Domain.Entities.Base;

namespace SurveySystem.Aplication
{
    /// <summary>
    /// Класс - входная точка проекта, регистрирующий реализованные зависимости текущим проектом
    /// </summary>
    public static class Entry
    {
        /// <summary>
        /// Добавить службы проекта с логикой
        /// </summary>
        /// <param name="services">Коллекция служб</param>
        /// <returns>Обновленная коллекция служб</returns>
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Entry));
            services.AddMediatR(typeof(BaseEntity));
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IDbSeeder, DbSeeder>();


            return services;
        }
    }
}
