using Domain.Entities;
using Domain.Entities.Sprints.Events;

namespace Application.Common.Factories
{
    public static class SprintNoticeFactory
    {
        public static Notice From(SprintStarted e)
        {
            return Notice.Create(
                senderId: SystemUser.Id, // system actor
                contextType: NoticeContext.Project,
                contextId: e.ProjectId,
                kind: NoticeKind.SystemEvent,
                severity: NoticeSeverity.Info,
                type: NoticeType.SprintStarted,
                message: $"Sprint started on {e.ActivatedAt:dd MMM yyyy}"
            );
        }

        public static Notice From(SprintCompleted e)
        {
            return Notice.Create(
                senderId: SystemUser.Id,
                contextType: NoticeContext.Project,
                contextId: e.ProjectId,
                kind: NoticeKind.SystemEvent,
                severity: NoticeSeverity.Success,
                type: NoticeType.SprintCompleted,
                message: $"Sprint completed"
            );
        }
    }
}
