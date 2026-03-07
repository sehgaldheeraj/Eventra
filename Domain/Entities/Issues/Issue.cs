using Domain.Common;
using Domain.Entities.Issues.Events;
using Domain.Entities.Projects;
using Domain.Entities.Sprints;
using System;
using System.Collections.Generic;

namespace Domain.Entities.Issues
{
    public class Issue : Entity
    {
        // ---- Identity ----
        public Guid Id { get; private set; }

        // ---- Core ----
        public string Title { get; private set; } = null!;
        public string Description { get; private set; } = string.Empty;

        // ---- Relationships ----
        public Guid? ParentIssueId { get; private set; }
        public Guid? SprintId { get; private set; }
        public Guid ProjectId { get; private set; }
        public Guid CreatedById { get; private set; }
        public Guid? AssigneeId { get; private set; }

        // ---- Navigation (query-only) ----
        public Issue? ParentIssue { get; private set; }
        public ICollection<Issue> SubIssues { get; private set; } = [];

        public Sprint? Sprint { get; private set; }
        public Project? Project { get; private set; }
        public User? Creator { get; private set; }
        public User? Assignee { get; private set; }

        // ---- State ----
        public DateTime CreatedAt { get; private set; }
        public DateTime? ClosedAt { get; private set; }
        public IssueStatus Status { get; private set; }

        private Issue() { } // EF

        // =========================
        // SINGLE CREATION ENTRY
        // =========================
        public static Issue Create(
            string title,
            string description,
            Guid projectId,
            Guid createdById,
            Guid? parentIssueId = null,
            Guid? sprintId = null,
            Guid? assigneeId = null)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title is required.", nameof(title));

            var issue = new Issue
            {
                Id = Guid.NewGuid(),
                Title = title,
                Description = description ?? string.Empty,
                ProjectId = projectId,
                CreatedById = createdById,
                ParentIssueId = parentIssueId,
                SprintId = sprintId,
                AssigneeId = assigneeId,
                CreatedAt = DateTime.UtcNow,
                Status = sprintId == null
                            ? IssueStatus.Backlog
                            : IssueStatus.ToDo
            };

            issue.AddDomainEvent(new IssueCreated(
                issue.Id,
                issue.ProjectId,
                issue.CreatedById,
                issue.Title,
                issue.ParentIssueId,
                issue.CreatedAt));

            return issue;
        }

        // =========================
        // DETAILS
        // =========================
        public void UpdateDetails(string title, string description)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title is required.", nameof(title));

            Title = title;
            Description = description ?? string.Empty;
        }

        // =========================
        // ASSIGNMENT
        // =========================
        public void AssignAssignee(Guid assigneeId, Guid assignerId)
        {
            if (AssigneeId == assigneeId)
                return;

            AssigneeId = assigneeId;

            AddDomainEvent(new IssueAssigned(
                Id,
                assigneeId,
                assignerId,
                ProjectId));
        }

        public void UnassignAssignee(Guid unassignedById)
        {
            if (AssigneeId == null)
                return;

            AssigneeId = null;

            AddDomainEvent(new IssueUnassigned(
                Id,
                ProjectId,
                unassignedById));
        }

        // =========================
        // SPRINT
        // =========================
        public void AssignToSprint(Guid sprintId, Guid addedById)
        {
            if (SprintId == sprintId)
                return;

            SprintId = sprintId;

            if (Status == IssueStatus.Backlog)
                Status = IssueStatus.ToDo;

            AddDomainEvent(new IssueAddedToSprint(
                Id,
                sprintId,
                ProjectId,
                addedById));
        }

        public void UnassignFromSprint(Guid removedById)
        {
            if (SprintId == null)
                return;

            var previousSprint = SprintId.Value;
            SprintId = null;

            Status = IssueStatus.Backlog;

            AddDomainEvent(new IssueRemovedFromSprint(
                Id,
                previousSprint,
                ProjectId,
                removedById));
        }

        // =========================
        // STATUS
        // =========================
        public void MoveToInProgress(Guid movedById)
        {
            if (AssigneeId == null)
                throw new InvalidOperationException(
                    "Cannot move to In Progress without an assignee.");

            if (Status == IssueStatus.Closed)
                throw new InvalidOperationException(
                    "Closed issues cannot be moved.");

            Status = IssueStatus.InProgress;

            var movedAt = DateTime.UtcNow;

            AddDomainEvent(new IssueMovedToInProgress(
                Id,
                ProjectId,
                movedById,
                movedAt));
        }

        public void Close(Guid closedById)
        {
            if (Status == IssueStatus.Closed)
                return;
            var closedAt = DateTime.UtcNow;
            ClosedAt = closedAt;
            Status = IssueStatus.Closed;

            AddDomainEvent(new IssueClosed(
                Id,
                ProjectId,
                closedById,
                closedAt));
        }

        public void Reopen(Guid reopenedById)
        {
            if (Status != IssueStatus.Closed)
                throw new InvalidOperationException(
                    "Only closed issues can be reopened.");

            ClosedAt = null;

            Status = SprintId == null
                ? IssueStatus.Backlog
                : IssueStatus.ToDo;

            AddDomainEvent(new IssueReopened(
                Id,
                ProjectId,
                reopenedById));
        }
    }

    public enum IssueStatus
    {
        Backlog = 0,
        ToDo = 1,
        InProgress = 2,
        Closed = 3
    }
}
