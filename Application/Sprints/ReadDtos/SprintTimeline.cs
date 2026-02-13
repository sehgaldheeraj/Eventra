using Domain.Entities.Sprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Sprints.ReadDtos
{
    public class SprintTimeline
    {
        public Guid SprintId { get; set; }
        public string Title { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public SprintStatus Status { get; set; }
        public int IssueCount { get; set; }
    }
}
