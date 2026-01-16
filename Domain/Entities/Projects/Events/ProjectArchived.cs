using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Projects.Events
{
    public class ProjectArchived(Guid projectId, string projectName, Guid deletedByUserId) : IDomainEvent
    {
        public Guid ProjectId { get; } = projectId;
        public string ProjectName { get; } = projectName;
        public Guid DeletedByUserId { get; } = deletedByUserId;
        public DateTime OccuredAt { get; set; } = DateTime.UtcNow;
    }
}
