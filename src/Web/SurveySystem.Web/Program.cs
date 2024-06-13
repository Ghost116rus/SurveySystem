using SurveySystem.Web.Swagger;
using SurveySystem.Aplication;
using SurveySystem.Services;
using SurveySystem.PosgreSQL;
using SurveySystem.PosgreSQL.Services;
using SurveySystem.Web.Middleware;
using SurveySystem.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configuration = builder.Configuration;

services
    .AddSwagger()
    .AddHttpContextAccessor()
    .AddServices()
    .AddCore()
    .AddPostgreSql(x => x.ConnectionString = configuration.GetConnectionString("DbConnectionString"))
    .AddBroker(configuration.GetSection("BrokerSettings").Get<BrokerOptions>())
    .AddCors(options => options.AddPolicy(
        "AllowAll",
        policy => {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
            policy.AllowCredentials();
        }));

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
    app.UseCors("AllowAll");

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
