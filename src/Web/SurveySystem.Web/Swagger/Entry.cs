using Microsoft.OpenApi.Models;

namespace SurveySystem.Web.Swagger
{
    /// <summary>
    /// Точка входа для сваггера
    /// </summary>
    public static class Entry
    {
        /// <summary>
        /// Добавить сваггер в службы приложения
        /// </summary>
        /// <param name="services">Службы приложения</param>
        /// <returns>Службы приложения со сваггером</returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services) =>
            services.AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddSwaggerGen(option =>
                {
                    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                    });
                    option.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer",
                                },
                            },
                            new string[] {}
                        },
                    });
                });
    }
}
