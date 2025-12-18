using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IIssueQueryRepository
    {
        Task<bool> IssueExistsAsync(Guid issueId, CancellationToken ct);    
    }
}
