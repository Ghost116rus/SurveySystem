using SurveySystem.Web.Swagger;
using SurveySystem.Aplication;
using SurveySystem.Services;
using SurveySystem.PosgreSQL;
using SurveySystem.PosgreSQL.Services;
using SurveySystem.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configuration = builder.Configuration;

services
    .AddSwagger()
    .AddHttpContextAccessor()
    .AddServices()
    .AddCore()
    .AddPostgreSql(x => x.ConnectionString = configuration.GetConnectionString("DbConnectionString"))
    .AddCors(options => options.AddPolicy(
        "AllowOrigin",
        builder => builder.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials()));



services.AddControllers();

var app = builder.Build();
{
    Console.WriteLine(configuration.GetConnectionString("DbConnectionString"));
    using (var scope = app.Services.CreateScope())
    {
        var migrator = scope.ServiceProvider.GetRequiredService<DbMigrator>();

        await migrator.MigrateAsync();
    }

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCustomExceptionHandler();
    app.UseCors("AllowOrigin");

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
