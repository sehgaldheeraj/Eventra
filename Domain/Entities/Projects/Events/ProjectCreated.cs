using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Projects.Events
{
    public class ProjectCreated : IDomainEvent
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public Guid CreatedByUserId { get; set; }
        public DateTime OccuredAt { get; set; } = DateTime.UtcNow;
        public ProjectCreated(Guid projectId, string projectName, Guid createdByUserId)
        {
            ProjectId = projectId;
            ProjectName = projectName;
            CreatedByUserId = createdByUserId;
        }
    }
}
