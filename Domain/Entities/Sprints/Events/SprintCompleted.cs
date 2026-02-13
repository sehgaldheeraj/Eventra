using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Sprints.Events
{
    public sealed record SprintCompleted(Guid ProjectId, Guid SprintId, DateTime CompletedAt) : DomainEvent;
}
