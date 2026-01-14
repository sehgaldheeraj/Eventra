using Domain.Entities;
using Domain.Entities.Projects.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                message: $"Project '{e.ProjectName}' was created"
            );
        }
    }

}
