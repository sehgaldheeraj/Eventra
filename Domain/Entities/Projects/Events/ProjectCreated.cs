using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Projects.Events
{
    public class ProjectCreated(Guid projectId, string projectName, Guid createdByUserId) : IDomainEvent
    {
        public Guid ProjectId { get; set; } = projectId;
        public string ProjectName { get; set; } = projectName;
        public Guid CreatedByUserId { get; set; } = createdByUserId;
        public DateTime OccuredAt { get; set; } = DateTime.UtcNow;
    }
}
