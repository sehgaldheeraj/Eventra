using Application.Common.Events;
using Application.Common.Factories;
using Domain.Entities.Sprints.Events;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Notices.EventHandlers.Sprint
{
    public class SprintCompletedNoticeHandler(INoticeRepository noticeRepository) : INotificationHandler<DomainEventNotification<SprintCompleted>>
    {
        private readonly INoticeRepository _noticeRepository = noticeRepository;
        public async Task Handle(DomainEventNotification<SprintCompleted> notification, CancellationToken ct)
        {
            var evt = notification.DomainEvent;
            var notice = SprintNoticeFactory.From(evt);
            await _noticeRepository.CreateNoticeAsync(notice, ct);
        }
    }
}
