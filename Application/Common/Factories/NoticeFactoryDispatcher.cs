using Application.Common.Exceptions;
using Application.Common.Factories;
using Domain.Common;
using Domain.Entities;
using Domain.Entities.Issues.Events;
using Domain.Entities.Projects.Events;
using Domain.Entities.Sprints.Events;
using NotSupportedException = Application.Common.Exceptions.NotSupportedException;


namespace Application.Notices.Factories
{
    public static class NoticeFactoryDispatcher
    {
        public static Notice From(DomainEvent domainEvent)
        {
            return domainEvent switch
            {
                // ===== ISSUE EVENTS =====
                IssueCreated e => IssueNoticeFactory.From(e),
                IssueAssigned e => IssueNoticeFactory.From(e),
                IssueUnassigned e => IssueNoticeFactory.From(e),
                IssueMovedToInProgress e => IssueNoticeFactory.From(e),
                IssueRemovedFromSprint e => IssueNoticeFactory.From(e),
                IssueReopened e => IssueNoticeFactory.From(e),
                IssueAddedToSprint e => IssueNoticeFactory.From(e),
                IssueClosed e => IssueNoticeFactory.From(e),

                // ===== SPRINT EVENTS =====
                SprintStarted e => SprintNoticeFactory.From(e),
                SprintCompleted e => SprintNoticeFactory.From(e),

                // ===== PROJECT EVENTS =====
                ProjectCreated e => ProjectNoticeFactory.From(e),

                _ => throw new NotSupportedException(domainEvent.GetType())
            };
        }
    }
}