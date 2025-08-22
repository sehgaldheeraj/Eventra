using Domain.Entities;
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
    public class IssueRepository : IIssueRepository
    {
        private readonly EventraDBContext _dbContext;
        public IssueRepository(EventraDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateIssueAsync(Issue issue, CancellationToken ct)
        {
            await _dbContext.Issues.AddAsync(issue, ct);
            await _dbContext.SaveChangesAsync(ct);  
        }
        public async Task<Issue?> GetIssueByIdAsync(Guid? Id, CancellationToken ct)
        {
            return await _dbContext.Issues.FirstOrDefaultAsync(u => u.Id == Id, ct);
        }
    }
}
