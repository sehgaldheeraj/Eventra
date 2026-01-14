using Application.Common.Interfaces.Dispatchers;
using Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Events
{
    public class DomainEventDispatcher(IMediator mediator) : IDomainEventDispatcher
    {
        private readonly IMediator _mediator = mediator;
        public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken ct = default)
        {
            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(
                    new DomainEventNotification<IDomainEvent>(domainEvent), ct);
            }
        }
    }
}
