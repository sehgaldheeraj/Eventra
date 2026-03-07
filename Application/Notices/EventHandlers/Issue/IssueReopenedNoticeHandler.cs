using Application.Common.Events;
using Application.Common.Factories;
using Domain.Common;
using Domain.Entities.Issues.Events;
using Domain.Interfaces;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Notices.EventHandlers.Issue
{
    public class IssueReopenedNoticeHandler(INoticeRepository noticeRepository) : INotificationHandler<DomainEventNotification<IssueReopened>>
    {
        private readonly INoticeRepository _noticeRepository = noticeRepository;

        public async Task Handle(DomainEventNotification<IssueReopened> notification, CancellationToken ct)
        {
            var evt = notification.DomainEvent;
            var notice = IssueNoticeFactory.From(evt);
            await _noticeRepository.CreateNoticeAsync(notice, ct);  
        }
    }
}
