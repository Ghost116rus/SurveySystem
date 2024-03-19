using SurveySystem.Domain.Interfaces;
using System.Collections.Concurrent;

namespace SurveySystem.Domain.Entities.Base
{
    /// <summary>
    /// Базовая сущность
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Доменные события
        /// </summary>
        private ConcurrentQueue<IDomainEvent> _domainEvents = default!;

        /// <summary>
        /// Id сущности
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Дата последнего изменения
        /// </summary>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// Добавить доменное событие
        /// </summary>
        /// <param name="domainEvent">событие</param>
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            if (domainEvent == null)
                return;

            _domainEvents ??= new ConcurrentQueue<IDomainEvent>();
            _domainEvents.Enqueue(domainEvent);
        }

        /// <summary>
        /// Получить все доменные события и очистить
        /// </summary>
        /// <returns>Список доменных событий</returns>
        public IEnumerable<IDomainEvent> RetrieveDomainEvents()
        {
            if (_domainEvents == null)
                return Enumerable.Empty<IDomainEvent>();

            var events = _domainEvents.ToList();
            _domainEvents.Clear();
            return events;
        }
    }
}
