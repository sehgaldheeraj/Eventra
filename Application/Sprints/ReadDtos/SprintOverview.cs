using Application.Issues.ReadDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Sprints.ReadDtos
{
    public class SprintOverview
    {
        public Guid SprintId { get; set; }
        public string Title { get; set; }
        public string Goal { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public SprintStatus Status { get; set; }

        // ---- Issue metadata ----
        public int TotalIssues { get; set; }
        public int BacklogCount { get; set; }
        public int ToDoCount { get; set; }
        public int InProgressCount { get; set; }
        public int ClosedCount { get; set; }

        public List<IssueSummary> Issues { get; set; } = [];
    }
}
