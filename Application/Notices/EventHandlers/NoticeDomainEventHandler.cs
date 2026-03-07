using Application.Common.Events;
using Application.Common.Factories;
using Application.Notices.Factories;
using Domain.Common;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Notices.EventHandlers
{
    public class NoticeDomainEventHandler<TEvent>(INoticeRepository noticeRepository) 
        : INotificationHandler<DomainEventNotification<TEvent>>
        where TEvent : DomainEvent
    {
        private readonly INoticeRepository _noticeRepository = noticeRepository;
        public async Task Handle(DomainEventNotification<TEvent> notification, CancellationToken ct)
        {
            var notice = NoticeFactoryDispatcher.From(notification.DomainEvent);
            await _noticeRepository.CreateNoticeAsync(notice, ct);
        }
    }
}
