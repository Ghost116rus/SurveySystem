using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SurveySystem.Aplication.Interfaces;
using System.Reflection;

namespace SurveySystem.PosgreSQL.Services
{
    /// <summary>
    /// Мигратор
    /// </summary>
    public class DbMigrator
    {
        private readonly EfContext _documentDbContext;
        private readonly ILogger<DbMigrator> _logger;
        private readonly IDbSeeder _dbSeeder;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="dbContext">Контекст БД</param>
        /// <param name="logger">Логгер</param>
        /// <param name="dbSeeder">Сервис добавления данных в БД</param>
        public DbMigrator(EfContext dbContext, ILogger<DbMigrator> logger, IDbSeeder dbSeeder)
        {
            _documentDbContext = dbContext;
            _logger = logger;
            _dbSeeder = dbSeeder;
        }

        /// <summary>
        /// Мигрировать БД
        /// </summary>
        /// <returns>Ничего</returns>
        public async Task MigrateAsync()
        {
            var operationId = Guid.NewGuid().ToString().Substring(0, 4);
            _logger.LogInformation($"UpdateDatabase:{operationId}: starting...");
            try
            {
                //await _documentDbContext.Database.EnsureDeletedAsync();
                await _documentDbContext.Database.MigrateAsync().ConfigureAwait(false);
                _logger.LogInformation($"UpdateDatabase:{operationId}: successfully done");
                await _dbSeeder.SeedAsync(_documentDbContext);
                _logger.LogInformation($"DbSeeder:{operationId}: successfully done");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"UpdateDatabase:{operationId}: failed");
                throw;
            }
        }
    }
}
