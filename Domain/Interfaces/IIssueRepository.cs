using Domain.Entities.Issues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IIssueRepository
    {
        Task CreateIssueAsync(Issue issue, CancellationToken cancellationToken);
        Task<Issue?> GetIssueByIdAsync(Guid? id,  CancellationToken cancellationToken);
        Task UpdateIssueAsync(Issue issue, CancellationToken ct);
        Task<IEnumerable<Issue>> GetIssuesAsync(
            Guid? parentIssueId,
            Guid? sprintId,
            Guid? userId,
            Guid? projectId,
            CancellationToken ct);
    }
}
