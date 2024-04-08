using Microsoft.Extensions.DependencyInjection;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Services.Authentication;


namespace SurveySystem.Services
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
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddUserContext()
                .AddCustomHeaderAuthentication(services)
                .AddSingleton<IDateTimeProvider, DateTimeProvider>()
                .AddScoped<IPasswordEncryptionService, PasswordEncryptionService>()
                .AddScoped<ITokenAuthenticationService, TokenAuthenticationService>()
                .AddScoped<IClaimsIdentityFactory, ClaimsIdentityFactory>();
        }
    }
}
