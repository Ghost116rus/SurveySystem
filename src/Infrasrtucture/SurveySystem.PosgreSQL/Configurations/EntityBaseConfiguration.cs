using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveySystem.Domain.Entities.Base;

namespace SurveySystem.PosgreSQL.Configurations
{
    /// <summary>
    /// Базовая конфигурация для базовой сущности <see cref="EntityBase"/>
    /// </summary>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    internal abstract class EntityBaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
         where TEntity : BaseEntity
    {
        protected const string ListEnumCommand = "'{}'::integer[]";
        protected const string ListGuidCommand = "'{}'::uuid[]";

        private const string GuidCommand = "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)";
        private const string NowCommand = "now() at time zone 'utc'";

        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            ConfigureId(builder);
            ConfigureBase(builder);
            ConfigureChild(builder);
        }

        /// <summary>
        /// Конфигурация сущности, не считая полей базового класса  <see cref="EntityBase"/>
        /// </summary>
        /// <param name="builder">Строитель конфигурации</param>
        public abstract void ConfigureChild(EntityTypeBuilder<TEntity> builder);

        /// <summary>
        /// Конфигурация идентификатора сущности
        /// </summary>
        /// <param name="builder">Строитель конфигурации</param>
        protected virtual void ConfigureId(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .IsRequired()
                .HasDefaultValueSql(GuidCommand);
        }

        private static void ConfigureBase(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.CreatedOn)
                .HasColumnName("created_date")
                .HasComment("Дата создания записи")
                .IsRequired()
                .HasDefaultValueSql(NowCommand);

            builder
                .Property(x => x.ModifiedOn)
                .HasColumnName("updated_date")
                .HasComment("Дата изменения записи")
                .IsRequired();

            builder.Ignore(p => p.IsNew);
        }
    }
}
