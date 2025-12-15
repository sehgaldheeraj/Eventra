using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Sprint
    {
        // ---- Identity ----
        public Guid Id { get; private set; }

        // ---- Core Fields ----
        public string Title { get; private set; }
        public string Goal { get; private set; }

        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        // ---- Relationships ----
        public Guid ProjectId { get; private set; }
        public Project? Project { get; private set; }

        public ICollection<Issue> Issues { get; private set; } = [];

        // ---- State ----
        public SprintStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? ActivatedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }

        private Sprint() { } // EF Core ctor

        public Sprint(string title, string goal, DateTime startDate, DateTime endDate, Guid projectId)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Sprint name is required.", nameof(title));

            if (endDate <= startDate)
                throw new ArgumentException("Sprint end date must be after start date.");

            Id = Guid.NewGuid();
            Title = title;
            Goal = goal ?? string.Empty;
            StartDate = startDate;
            EndDate = endDate;
            CreatedAt = DateTime.UtcNow;

            ProjectId = projectId;

            Status = SprintStatus.Planned; // explicit default
        }

        // ---- Core Mutators ----
        public void UpdateDetails(
           Guid? projectId = null,
           string? title = null,
           string? goal = null,
           DateTime? startDate = null,
           DateTime? endDate = null,
           SprintStatus? status = null
       )
        {
            if (!string.IsNullOrWhiteSpace(title))
                Title = title;

            if (goal != null)
                Goal = goal;

            if (startDate.HasValue)
                StartDate = startDate.Value;

            if (endDate.HasValue)
                EndDate = endDate.Value;

            if (startDate.HasValue || endDate.HasValue)
            {
                if (EndDate <= StartDate)
                    throw new ArgumentException("Sprint end date must be after start date.");
            }

            if (projectId.HasValue)
                ProjectId = projectId.Value;

            if (status.HasValue)
                Status = status.Value;
        }

        public void DeleteSprint()
        {
            if (Status == SprintStatus.Completed)
                throw new InvalidOperationException("Cannot delete a completed sprint.");
        }
        // ---- Status Transitions ----
        public void Start()
        {
            if (Status != SprintStatus.Planned)
                throw new InvalidOperationException("Only planned sprints can be started.");

            ActivatedAt = DateTime.UtcNow;
            Status = SprintStatus.Active;
        }

        public void Complete()
        {
            if (Status != SprintStatus.Active)
                throw new InvalidOperationException("Only active sprints can be completed.");

            CompletedAt = DateTime.UtcNow;
            Status = SprintStatus.Completed;
        }

        public void Cancel()
        {
            if (Status == SprintStatus.Completed)
                throw new InvalidOperationException("Cannot cancel a completed sprint.");

            Status = SprintStatus.Cancelled;
        }

        public void Reopen()
        {
            if (Status != SprintStatus.Completed && Status != SprintStatus.Cancelled)
                throw new InvalidOperationException("Only completed or cancelled sprints can be reopened.");

            CompletedAt = null;
            ActivatedAt = null;
            Status = SprintStatus.Planned;
        }

    }
    public enum SprintStatus
    {
        Planned = 0,
        Active = 1,
        Completed = 2,
        Cancelled = 3
    }
}
