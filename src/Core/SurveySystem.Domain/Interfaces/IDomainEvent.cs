using MediatR;

namespace SurveySystem.Domain.Interfaces
{
    public interface IDomainEvent : INotification
    {
        /// <summary>
        /// Обрабатывать событие в транзакции
        /// </summary>
        public bool IsInTransaction { get; }
    }
}
