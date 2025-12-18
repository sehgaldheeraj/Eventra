using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Issue
    {
        // ---- Identity ----
        public Guid Id { get; private set; }

        // ---- Core Fields ----
        public string Title { get; private set; } = null!;
        public string Description { get; private set; } = string.Empty;

        // ---- Relationships (FKs are the truth) ----
        public Guid? ParentIssueId { get; private set; }
        public Guid? SprintId { get; private set; }
        public Guid ProjectId { get; private set; }
        public Guid AssignerId { get; private set; }
        public Guid? AssigneeId { get; private set; }

        // ---- Navigation Properties (query-only) ----
        public Issue? ParentIssue { get; private set; }
        public ICollection<Issue> SubIssues { get; private set; } = [];

        public Sprint? Sprint { get; private set; }
        public Project? Project { get; private set; }
        public User? Assigner { get; private set; }
        public User? Assignee { get; private set; }

        // ---- State ----
        public DateTime CreatedAt { get; private set; }
        public DateTime? ClosedAt { get; private set; }
        public IssueStatus Status { get; private set; }

        private Issue() { } // EF Core

        // ---- Constructor (IDs only) ----
        public Issue(
            string title,
            string description,
            Guid projectId,
            Guid assignerId)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title is required.", nameof(title));

            Id = Guid.NewGuid();
            Title = title;
            Description = description ?? string.Empty;
            ProjectId = projectId;
            AssignerId = assignerId;

            CreatedAt = DateTime.UtcNow;
            Status = IssueStatus.Backlog;
        }

        // ---- Details ----
        public void UpdateDetails(string title, string description)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title is required.", nameof(title));

            Title = title;
            Description = description ?? string.Empty;
        }

        // ---- Assignee ----
        public void AssignAssignee(Guid assigneeId)
        {
            AssigneeId = assigneeId;
        }

        public void UnassignAssignee()
        {
            AssigneeId = null;
        }

        // ---- Sub Issue ----
        public void SetAsSubIssue(Guid parentIssueId)
        {
            ParentIssueId = parentIssueId;
        }

        public void RemoveFromParent()
        {
            ParentIssueId = null;
        }

        // ---- Sprint ----
        public void AssignToSprint(Guid sprintId)
        {
            SprintId = sprintId;
        }

        public void UnassignFromSprint()
        {
            SprintId = null;
        }

        // ---- Status Transitions ----
        public void MoveToBacklog()
        {
            if (Status == IssueStatus.Closed)
                throw new InvalidOperationException(
                    "Cannot move a closed issue back to Backlog. Reopen first.");

            Status = IssueStatus.Backlog;
        }

        public void MoveToToDo()
        {
            if (SprintId == null)
                throw new InvalidOperationException(
                    "Cannot move to To Do without being in a Sprint.");

            if (Status == IssueStatus.Closed)
                throw new InvalidOperationException(
                    "Closed issues cannot be moved. Reopen first.");

            Status = IssueStatus.ToDo;
        }

        public void MoveToInProgress()
        {
            if (AssigneeId == null)
                throw new InvalidOperationException(
                    "Cannot move to In Progress without an Assignee.");

            if (Status == IssueStatus.Closed)
                throw new InvalidOperationException(
                    "Closed issues cannot be moved. Reopen first.");

            Status = IssueStatus.InProgress;
        }

        public void Close()
        {
            if (Status == IssueStatus.Closed)
                return;

            ClosedAt = DateTime.UtcNow;
            Status = IssueStatus.Closed;
        }

        public void Reopen()
        {
            if (Status != IssueStatus.Closed)
                throw new InvalidOperationException(
                    "Only closed issues can be reopened.");

            ClosedAt = null;
            Status = SprintId == null ? IssueStatus.Backlog : IssueStatus.ToDo;
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
