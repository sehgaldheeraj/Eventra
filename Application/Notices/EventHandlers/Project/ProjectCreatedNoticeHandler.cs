using Application.Common.Events;
using Application.Common.Factories;
using Domain.Entities;
using Domain.Entities.Projects.Events;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace Application.Notices.EventHandlers.Project
{
    public class ProjectCreatedNoticeHandler(INoticeRepository noticeRepository) : INotificationHandler<DomainEventNotification<ProjectCreated>>
    {
        private readonly INoticeRepository _noticeRepository = noticeRepository;
        public async Task Handle(DomainEventNotification<ProjectCreated> notification, CancellationToken ct)
        {
            var evt = notification.DomainEvent;
            var notice = ProjectNoticeFactory
                .From(evt);
            await _noticeRepository.CreateNoticeAsync(notice, ct);
        }
    }
}
