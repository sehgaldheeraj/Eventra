using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Projects.Events
{
    public sealed record ProjectCreated(Guid ProjectId, string ProjectName, Guid CreatedByUserId): DomainEvent;
}
