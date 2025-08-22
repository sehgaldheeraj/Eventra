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
        public ICollection<Issue> SubIssues { get; private set; } = new List<Issue>();

        public Guid? SprintId { get; private set; }
        public Sprint? Sprint { get; private set; }

        public Guid? AssignerId { get; private set; }
        public User? Assigner { get; private set; }

        public Guid? AssigneeId { get; private set; }
        public User? Assignee { get; private set; }

        // public ICollection<Comment> Comments { get; private set; } = new List<Comment>();

        // ---- State ----
        public DateTime CreatedAt { get; private set; }
        public DateTime? ClosedAt { get; private set; }

        public IssueStatus Status { get; private set; }

        private Issue() { } // EF Core ctor

        // ---- Constructor ----
        public Issue(string title, string description)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            CreatedAt = DateTime.UtcNow;
            UpdateStatus();
        }

        // ---- Domain Behaviors ----

        public void UpdateDetails(string title, string description)
        {
            Title = title;
            Description = description;
        }

        // ----- Parent Issue -----
        public void AssignParent(Guid? parentIssueId)
        {
            ParentIssueId = parentIssueId;
            UpdateStatus();
        }

        // ----- Sprint -----
        public void AssignToSprint(Guid sprintId, Sprint sprint)
        {
            SprintId = sprintId;
            Sprint = sprint;
            UpdateStatus();
        }

        public void UnassignFromSprint()
        {
            SprintId = null;
            Sprint = null;
            UpdateStatus();
        }

        // ----- Assignee (worker) -----
        public void AssignAssignee(Guid assigneeId, User assignee)
        {
            AssigneeId = assigneeId;
            Assignee = assignee;
            UpdateStatus();
        }

        public void UnassignAssignee()
        {
            AssigneeId = null;
            Assignee = null;
            UpdateStatus();
        }

        // ----- Assigner (delegator) -----
        public void SetAssigner(Guid assignerId, User assigner)
        {
            AssignerId = assignerId;
            Assigner = assigner;
            // Assigner doesn’t affect workflow state → no UpdateStatus()
        }

        public void ClearAssigner()
        {
            AssignerId = null;
            Assigner = null;
        }

        // ----- Status Transitions -----
        public void Close()
        {
            ClosedAt = DateTime.UtcNow;
            UpdateStatus();
        }

        public void Reopen()
        {
            ClosedAt = null;
            UpdateStatus();
        }

        // ---- Invariant Enforcement ----
        private void UpdateStatus()
        {
            if (SprintId == null)
                Status = IssueStatus.Backlog;
            else if (ClosedAt.HasValue)
                Status = IssueStatus.Closed;
            else if (AssigneeId == null)
                Status = IssueStatus.ToDo;
            else
                Status = IssueStatus.InProgress;
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
