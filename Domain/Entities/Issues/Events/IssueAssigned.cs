using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Issues.Events
{
    public sealed record IssueAssigned(
        Guid IssueId,
        Guid ActorId,
        Guid AssigneeId,
        Guid ProjectId
        ) : DomainEvent(ActorId);
}
