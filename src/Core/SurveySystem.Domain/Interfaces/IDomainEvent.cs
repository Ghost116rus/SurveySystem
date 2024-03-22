using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
