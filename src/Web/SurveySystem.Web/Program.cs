using SurveySystem.Web.Swagger;
using SurveySystem.Aplication;
using SurveySystem.PosgreSQL;
using SurveySystem.Web.Authentication;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configuration = builder.Configuration;

services
    .AddSwagger()
    .AddHttpContextAccessor()
    .AddUserContext()
    .AddCore()
    .AddPostgreSql(x => x.ConnectionString = configuration.GetConnectionString("DbConnectionString"));



services.AddControllers();

var app = builder.Build();
{
    using (var scope = app.Services.CreateScope())
    {
        //var migrator = scope.ServiceProvider.GetRequiredService<DbMigrator>();
        //var s3Helper = scope.ServiceProvider.GetRequiredService<S3Helper>();

        //await migrator.MigrateAsync();
        //await s3Helper.PrepareAsync();
    }

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors("AllowOrigin");

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
