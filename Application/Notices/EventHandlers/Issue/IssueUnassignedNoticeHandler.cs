using Application.Common.Events;
using Application.Common.Factories;
using Domain.Entities.Issues.Events;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Notices.EventHandlers.Issue
{
    public class IssueUnassignedNoticeHandler(INoticeRepository noticeRepository) : INotificationHandler<DomainEventNotification<IssueUnassigned>>
    {
        private readonly INoticeRepository _noticeRepository = noticeRepository;

        public async Task Handle(DomainEventNotification<IssueUnassigned> notification, CancellationToken ct)
        {
            var evt = notification.DomainEvent;
            var notice = IssueNoticeFactory.From(evt);
            await _noticeRepository.CreateNoticeAsync(notice, ct);
        }
    }
}
