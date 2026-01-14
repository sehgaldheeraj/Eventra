using Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Events
{
    public class DomainEventNotification<TDomainEvent>(TDomainEvent domainEvent) : INotification
        where TDomainEvent : IDomainEvent
    {
        public TDomainEvent DomainEvent { get; } = domainEvent;
        
    }
}
