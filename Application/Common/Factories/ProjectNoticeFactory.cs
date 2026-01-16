using Domain.Entities;
using Domain.Entities.Projects.Events;

namespace Application.Common.Factories
{
    public static class ProjectNoticeFactory
    {
        public static Notice From(ProjectCreated e)
        {
            return Notice.Create(
                senderId: e.CreatedByUserId,
                contextType: NoticeContext.Project,
                contextId: e.ProjectId,
                kind: NoticeKind.SystemEvent,
                severity: NoticeSeverity.Success,
                type: NoticeType.ProjectCreated,
                message: $"Project '{e.ProjectName}' was created"
            );
        }

        public static Notice From(ProjectArchived e)
        {
            return Notice.Create(
                senderId: e.DeletedByUserId,
                contextType: NoticeContext.Project,
                contextId: e.ProjectId,
                kind: NoticeKind.SystemEvent,
                severity: NoticeSeverity.Warning,
                type: NoticeType.ProjectArchived,
                message: $"Project '{e.ProjectName}' was archived"
            );
        }

        public static Notice From(ProjectOwnerChanged e)
        {
            return Notice.Create(
                senderId: e.UpdatedOwnerId,
                contextType: NoticeContext.Project,
                contextId: e.ProjectId,
                kind: NoticeKind.SystemEvent,
                severity: NoticeSeverity.Info,
                type: NoticeType.ProjectOwnerChanged,
                message: $"You are now the owner of project '{e.ProjectName}'"
            );
        }
    }
}
