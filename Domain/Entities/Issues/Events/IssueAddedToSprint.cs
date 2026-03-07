using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Issues.Events
{
    public sealed record IssueAddedToSprint(
        Guid IssueId,
        Guid ProjectId,
        Guid SprintId,
        Guid ActorId) : DomainEvent(ActorId);
}
