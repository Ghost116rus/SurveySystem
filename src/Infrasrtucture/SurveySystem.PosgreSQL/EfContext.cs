using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Npgsql;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.Entities.Base;
using SurveySystem.Domain.Entities.Organization;
using SurveySystem.Domain.Entities.Surveys;
using SurveySystem.Domain.Entities.Users;
using SurveySystem.Domain.Exceptions;
using SurveySystem.Domain.Interfaces;

namespace SurveySystem.PosgreSQL
{
    /// <summary>
    /// Контекст EF Core для приложения
    /// </summary>
    public class EfContext : DbContext, IDbContext
    {
        private const string DefaultSchema = "public";
        private readonly IUserContext _userContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IMediator _domainEventsDispatcher;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="options">Параметры подключения к БД</param>
        /// <param name="userContext">Контекст текущего пользователя</param>
        /// <param name="dateTimeProvider">Провайдер даты и времени</param>
        /// <param name="domainEventsDispatcher">Медиатор для доменных событий</param>
        public EfContext(
            DbContextOptions<EfContext> options,
            IUserContext userContext,
            IDateTimeProvider dateTimeProvider,
            IMediator domainEventsDispatcher)
            : base(options)
        {
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
            _domainEventsDispatcher = domainEventsDispatcher ?? throw new ArgumentNullException(nameof(domainEventsDispatcher));
        }
        #region UsersEntities

        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentAnswer> StudentAnswers { get; set; }
        public DbSet<StudentCharacteristic> StudentCharacteristic { get; set; }
        public DbSet<StudentSurveyProgress> SurveyProgress { get; set; }

        #endregion

        #region SurveyEntities

        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerCharacteristicValue> AnswerCharacteristicValues { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionEvaluationCriteria> QuestionEvaluationCriteries { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyTestQuestion> SurveysTestQuestions { get; set; }

        #endregion

        #region OrganizationEntities

        public DbSet<Institute> Institutes { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Semester> Semesters { get; set; }

        #endregion

        /// <inheritdoc/>
        public bool IsInMemory => Database.IsInMemory();

        public Guid AdminId { get; set; }


        /// <inheritdoc cref="IDbContext.SaveChangesAsync(bool, CancellationToken)" />
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entityEntries = ChangeTracker.Entries().ToArray();
            if (entityEntries.Length > 10)
                entityEntries.AsParallel().ForAll(OnSave);
            else
                foreach (var entityEntry in entityEntries)
                    OnSave(entityEntry);

            // перед применением событий получаем их все из доменных сущностей во избежание дубликации в рекурсии
            var domainEvents = entityEntries
                .Select(po => po.Entity)
                .OfType<BaseEntity>()
                .SelectMany(x => x.RetrieveDomainEvents())
                .ToArray();

            try
            {
                var isNewTransaction = Database.CurrentTransaction is null;

                if (isNewTransaction)
                    await Database.BeginTransactionAsync(cancellationToken);

                await PublishEventsAsync(domainEvents.Where(x => x.IsInTransaction), cancellationToken);
                var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

                if (isNewTransaction)
                {
                    await Database.CommitTransactionAsync(cancellationToken);
                    await PublishEventsAsync(domainEvents.Where(x => !x.IsInTransaction), cancellationToken);
                }

                return result;
            }
            catch (DbUpdateException ex)
            {
                if (Database.CurrentTransaction is not null)
                    await Database.RollbackTransactionAsync(cancellationToken);

                return HandleDbUpdateException(ex, cancellationToken);
            }
        }

        /// <inheritdoc cref="IDbContext.SaveChangesAsync(CancellationToken)" />
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            await SaveChangesAsync(true, cancellationToken);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema(DefaultSchema);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EfContext).Assembly);
        }

        protected virtual int HandleDbUpdateException(DbUpdateException ex, CancellationToken cancellationToken = default)
        {
            if (ex?.InnerException is PostgresException postgresEx)
                throw postgresEx.SqlState switch
                {
                    PostgresErrorCodes.ForeignKeyViolation => new ExceptionBase(
                        $"Заданы некорректные идентификаторы для внешних ключей сущности: {postgresEx.Detail}", ex),
                    PostgresErrorCodes.UniqueViolation => new DuplicateUniqueKeyException(ex),
                    _ => ex,
                };
            throw ex ?? throw new ArgumentNullException(nameof(ex));
        }

        private void OnSave(EntityEntry entityEntry)
        {
            // TODO: вынести в домен
            if (entityEntry.State != EntityState.Unchanged)
            {
                UpdateTimestamp(entityEntry);
                SetModifiedUser(entityEntry);
            }
        }

        private void UpdateTimestamp(EntityEntry entityEntry)
        {
            var entity = entityEntry.Entity;
            if (entity is null)
                return;

            if (entity is BaseEntity table)
            {
                table.ModifiedOn = _dateTimeProvider.UtcNow;

                if (entityEntry.State == EntityState.Added && table.CreatedOn == DateTime.MinValue)
                    table.CreatedOn = _dateTimeProvider.UtcNow;
            }
        }

        private async Task PublishEventsAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken)
        {
            foreach (var @event in domainEvents)
                await _domainEventsDispatcher.Publish(@event, cancellationToken);
        }

        private void SetModifiedUser(EntityEntry entityEntry)
        {
            if (entityEntry?.Entity != null
                && entityEntry.State != EntityState.Unchanged
                && entityEntry.Entity is IUserTrackable userTrackable)
            {
                userTrackable.ModifiedByUserId = AdminId == default ? _userContext.CurrentUserId : AdminId;

                if (entityEntry.State == EntityState.Added)
                {
                    if (IsInMemory && userTrackable.CreatedByUserId != default)
                        return;
                    userTrackable.CreatedByUserId = AdminId == default ? _userContext.CurrentUserId : AdminId;
                }
            }
        }
    }
}
