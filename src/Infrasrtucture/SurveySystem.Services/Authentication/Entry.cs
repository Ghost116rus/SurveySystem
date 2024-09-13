using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.Enums;

namespace SurveySystem.Services.Authentication
{
    /// <summary>
    /// Точка входа сервисов аутентификации для приложения
    /// </summary>
    public static class Entry
    {
        /// <summary>
        /// Добавить контекст пользователя в службы приложения
        /// </summary>
        /// <param name="services">Службы приложения</param>
        /// <returns>Службы приложения с контекстом текущего пользователя</returns>
        public static IServiceCollection AddUserContext(this IServiceCollection services) =>
            services
                .AddScoped<IUserContext, UserContext>();

        /// <summary>
        /// Добавить аутентификацию по хэдерам
        /// </summary>
        /// <param name="builder">Строитель</param>
        /// <param name="services">Сервисы</param>
        /// <returns>Строитель аутентификации с аутентификацией</returns>
        public static IServiceCollection AddCustomHeaderAuthentication(this IServiceCollection builder, IServiceCollection services)
            => builder
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    var tokenService = services.BuildServiceProvider().GetRequiredService<ITokenAuthenticationService>();
                    options.Authority = tokenService!.Authority;
                    options.Audience = tokenService.Audience;
                    options.ClaimsIssuer = tokenService.ClaimsIssuer;
                    options.TokenValidationParameters = tokenService.GetTokenValidationParameters(TokenTypes.Auth);
                    options.Configuration = new OpenIdConnectConfiguration();
                })
                .Services
                .AddScoped<IPasswordEncryptionService, PasswordEncryptionService>();
    }
}
