using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Issues.Events
{
    public sealed record IssueMovedToInProgress(
        Guid IssueId,
        Guid ProjectId,
        Guid ActorId,
        DateTime MovedAt) : DomainEvent(ActorId);
}
