using Application.Sprints.ReadDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Projects.ReadDtos
{
    public class ProjectOverview
    {
        public Guid ProjectId { get; set; }
        public string Name { get; set; } 
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public string? OwnerName { get; set; }
        public string? OwnerRole { get; set; } 
        
        public int TotalIssues { get; set; }
        public int OpenIssues { get; set; }
        public int TotalSprints { get; set; }
        public Guid? OpenSprintId { get; set; }
        public bool HasOpenSprint { get; set; }
        public List<SprintTimeline> Sprints { get; set; } = [];
    }
}
