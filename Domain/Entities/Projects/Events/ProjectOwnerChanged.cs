using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Projects.Events
{
    public class ProjectOwnerChanged(Guid projectId, string projectName, Guid oldOwnerId, Guid updatedOwnerId) : IDomainEvent
    {
        public Guid ProjectId { get; } = projectId;
        public string ProjectName { get; } = projectName;
        public Guid UpdatedOwnerId { get; } = updatedOwnerId;
        public Guid OldOwnerId { get; } = oldOwnerId;
        public DateTime OccuredAt { get; set; } = DateTime.UtcNow;
    }
}
