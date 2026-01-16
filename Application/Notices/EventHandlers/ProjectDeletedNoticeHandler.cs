using Application.Common.Events;
using Domain.Entities.Projects.Events;
using Application.Common.Factories;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Notices.EventHandlers
{
    public class ProjectDeletedNoticeHandler(INoticeRepository noticeRepository) : INotificationHandler<DomainEventNotification<ProjectArchived>>
    {
        private readonly INoticeRepository _noticeRepository = noticeRepository;
        public async Task Handle(DomainEventNotification<ProjectArchived> notification, CancellationToken ct)
        {
            var evt = notification.DomainEvent;
            var notice = ProjectNoticeFactory
                .From(evt);
            await _noticeRepository.CreateNoticeAsync(notice, ct);
        }
    }
}
