using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Issue
    {
        // ---- Identity ----
        public Guid Id { get; private set; }

        // ---- Core Fields ----
        public string Title { get; private set; }
        public string Description { get; private set; }

        // ---- Relationships ----
        public Guid? ParentIssueId { get; private set; }
        public Issue? ParentIssue { get; private set; }
        public ICollection<Issue> SubIssues { get; private set; } = [];

        public Guid? SprintId { get; private set; }
        public Sprint? Sprint { get; private set; }

        public Guid ProjectId {get; private set; }
        public Project? Project { get; private set; }

        public Guid AssignerId { get; private set; }
        public User Assigner { get; private set; }

        public Guid? AssigneeId { get; private set; }
        public User? Assignee { get; private set; }

        // public ICollection<Comment> Comments { get; private set; } = new List<Comment>();

        // ---- State ----
        public DateTime CreatedAt { get; private set; }
        public DateTime? ClosedAt { get; private set; }

        public IssueStatus Status { get; private set; }

        private Issue() { } // EF Core ctor

        // ---- Constructor ----
        public Issue(string title, string description, Guid assignerId, User assigner, Guid projectId, Project project)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title is required.", nameof(title));

            Id = Guid.NewGuid();
            Title = title;
            Description = description ?? string.Empty;
            CreatedAt = DateTime.UtcNow;

            AssignerId = assignerId;
            Assigner = assigner ?? throw new ArgumentNullException(nameof(assigner));

            ProjectId = projectId;
            Project = project;

            Status = IssueStatus.Backlog; // explicit default
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
        public void AssignAssignee(Guid assigneeId, User assignee)
        {
            AssigneeId = assigneeId;
            Assignee = assignee ?? throw new ArgumentNullException(nameof(assignee));
        }

        public void UnassignAssignee()
        {
            AssigneeId = null;
            Assignee = null;
        }

        // ____ Sub Issue ----
        public void SetAsSubIssue(Guid issueId, Issue issue)
        {
            ParentIssueId = issueId;
            ParentIssue = issue ?? throw new ArgumentNullException(nameof(issue));
        }

        // ---- Sprint ----
        public void AssignToSprint(Guid sprintId, Sprint sprint)
        {
            SprintId = sprintId;
            Sprint = sprint ?? throw new ArgumentNullException(nameof(sprint));
        }

        public void UnassignFromSprint()
        {
            SprintId = null;
            Sprint = null;
        }

        // ---- Status Transitions ----
        public void MoveToBacklog()
        {
            if (Status == IssueStatus.Closed)
                throw new InvalidOperationException("Cannot move a closed issue back to Backlog. Reopen first.");

            Status = IssueStatus.Backlog;
        }

        public void MoveToToDo()
        {
            if (SprintId == null)
                throw new InvalidOperationException("Cannot move to To Do without being in a Sprint.");

            if (Status == IssueStatus.Closed)
                throw new InvalidOperationException("Closed issues cannot be moved. Reopen first.");

            Status = IssueStatus.ToDo;
        }

        public void MoveToInProgress()
        {
            if (AssigneeId == null)
                throw new InvalidOperationException("Cannot move to In Progress without an Assignee.");

            if (Status == IssueStatus.Closed)
                throw new InvalidOperationException("Closed issues cannot be moved. Reopen first.");

            Status = IssueStatus.InProgress;
        }

        public void Close()
        {
            if (Status == IssueStatus.Closed)
                return; // already closed

            ClosedAt = DateTime.UtcNow;
            Status = IssueStatus.Closed;
        }

        public void Reopen()
        {
            if (Status != IssueStatus.Closed)
                throw new InvalidOperationException("Only closed issues can be reopened.");

            ClosedAt = null;

            // 🟢 You can decide logic here: reopen to backlog always,
            // or if Sprint is set, reopen to ToDo.
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
