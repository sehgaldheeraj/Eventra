using Application.Common.Interfaces.QueryRepositories;
using Domain.Entities.Issues;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class IssueRepository(EventraDBContext dbContext) : IIssueRepository, IIssueQueryRepository
    {
        private readonly EventraDBContext _dbContext = dbContext;

        public async Task CreateIssueAsync(Issue issue, CancellationToken ct)
        {
            await _dbContext.Issues.AddAsync(issue, ct);
            await _dbContext.SaveChangesAsync(ct);  
        }
        public async Task<Issue?> GetIssueByIdAsync(Guid? Id, CancellationToken ct)
        {
            return await _dbContext.Issues.FirstOrDefaultAsync(u => u.Id == Id, ct);
        }
        public async Task UpdateIssueAsync(Issue issue, CancellationToken ct)
        {
            _dbContext.Issues.Update(issue);
            await _dbContext.SaveChangesAsync(ct);
        }
        public async Task<bool> IssueExistsAsync(Guid id, CancellationToken ct)
        {
            return await _dbContext.Issues.AnyAsync(u => u.Id == id, ct);    
        }
        public async Task<IEnumerable<Issue>> GetIssuesAsync(
            Guid? parentIssueId,
            Guid? sprintId,
            Guid? userId,
            Guid? projectId,
            CancellationToken ct)
        {
            // Start queryable
            var query = _dbContext.Issues.AsQueryable();

            // Apply only one filter per query
            if (parentIssueId.HasValue)
                query = query.Where(i => i.ParentIssueId == parentIssueId.Value);
            else if (sprintId.HasValue)
                query = query.Where(i => i.SprintId == sprintId.Value);
            else if (userId.HasValue)
                query = query.Where(i => i.AssigneeId == userId.Value);
            else if (projectId.HasValue)
                query = query.Where(i => i.ProjectId == projectId.Value);
            else
                throw new ArgumentException("At least one filter ID must be provided.");

            return await query.ToListAsync(ct);
        }
    }
}
