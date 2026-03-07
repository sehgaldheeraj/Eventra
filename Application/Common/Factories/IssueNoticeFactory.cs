using Application.Common.Events;
using Domain.Entities;
using Domain.Entities.Issues.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Factories
{
    internal static partial class IssueNoticeFactory
    {
        public static Notice From(IssueCreated e)
        {
            var isSubIssue = e.ParentIssueId.HasValue;

            var contextType = isSubIssue
                ? NoticeContext.Issue
                : NoticeContext.Project;

            var contextId = isSubIssue
                ? e.ParentIssueId!.Value
                : e.ProjectId;

            return Notice.Create(
                senderId: e.ActorId,
                contextType: contextType,
                contextId: contextId,
                kind: NoticeKind.StructuralLifecycle,
                severity: NoticeSeverity.Info,
                type: NoticeType.IssueCreated,
                message: e.Title // minimal payload, UI composes narrative
            );
        }
        public static Notice From(IssueAssigned e)
        {
            return Notice.Create(
                senderId: e.ActorId,
                contextType: NoticeContext.Project,
                contextId: e.ProjectId,
                kind: NoticeKind.Assignment,
                severity: NoticeSeverity.Info,
                type: NoticeType.IssueAssigned,
                message: $"Issue {e.IssueId} was assigned."
            );
        }
        public static Notice From(IssueMovedToInProgress e)
        {
            return Notice.Create(
                senderId: e.ActorId,
                contextType: NoticeContext.Project,
                contextId: e.ProjectId,
                kind: NoticeKind.StatusChange,
                severity: NoticeSeverity.Info,
                type: NoticeType.IssueMovedToInProgress,
                message: $"Issue {e.IssueId} moved to In Progress."
            );
        }
        public static Notice From(IssueClosed e)
        {
            return Notice.Create(
                senderId: e.ActorId,
                contextType: NoticeContext.Project,
                contextId: e.ProjectId,
                kind: NoticeKind.StatusChange,
                severity: NoticeSeverity.Info,
                type: NoticeType.IssueClosed,
                message: $"Issue {e.IssueId} was closed."
            );
        }
        public static Notice From(IssueReopened e)
        {
            return Notice.Create(
                senderId: e.ActorId,
                contextType: NoticeContext.Project,
                contextId: e.ProjectId,
                kind: NoticeKind.StatusChange,
                severity: NoticeSeverity.Info,
                type: NoticeType.IssueReopened,
                message: $"Issue {e.IssueId} was reopened."
            );
        }
        public static Notice From(IssueAddedToSprint e)
        {
            return Notice.Create(
                senderId: e.ActorId,
                contextType: NoticeContext.Sprint,
                contextId: e.SprintId,
                kind: NoticeKind.StructuralLifecycle,
                severity: NoticeSeverity.Info,
                type: NoticeType.IssueAddedToSprint,
                message: $"Issue {e.IssueId} was added to sprint."
            );
        }

        public static Notice From(IssueRemovedFromSprint e)
        {
            return Notice.Create(
                senderId: e.ActorId,
                contextType: NoticeContext.Sprint,
                contextId: e.SprintId,
                kind: NoticeKind.StructuralLifecycle,
                severity: NoticeSeverity.Info,
                type: NoticeType.IssueRemovedFromSprint,
                message: $"Issue {e.IssueId} was removed from sprint."
            );
        }
        public static Notice From(IssueUnassigned e)
        {
            return Notice.Create(
                senderId: e.ActorId,
                contextType: NoticeContext.Project,
                contextId: e.ProjectId,
                kind: NoticeKind.Assignment,
                severity: NoticeSeverity.Info,
                type: NoticeType.IssueUnassigned,
                message: $"Issue {e.IssueId} was unassigned."
            );
        }
    }
}
