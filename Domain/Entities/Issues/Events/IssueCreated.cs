using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Issues.Events
{
    public sealed record IssueCreated(
        Guid IssueId,
        Guid ProjectId,
        Guid AssignerId,
        string Title,
        Guid? ParentIssueId,
        DateTime CreatedAt
    ) : DomainEvent;
}
