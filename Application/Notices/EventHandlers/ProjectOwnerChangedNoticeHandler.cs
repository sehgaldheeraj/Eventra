using Application.Common.Events;
using Application.Common.Factories;
using Domain.Entities;
using Domain.Entities.Projects.Events;
using Domain.Interfaces;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Notices.EventHandlers
{
    public class ProjectOwnerChangedNoticeHandler(INoticeRepository noticeRepository) : INotificationHandler<DomainEventNotification<ProjectOwnerChanged>>
    {
        private readonly INoticeRepository _noticeRepository = noticeRepository;
        public async Task Handle(DomainEventNotification<ProjectOwnerChanged> notification, CancellationToken ct)
        {
            var evt = notification.DomainEvent;
            var notice = ProjectNoticeFactory.From(evt);
            await _noticeRepository.CreateNoticeAsync(notice, ct);
        }
    }
}
