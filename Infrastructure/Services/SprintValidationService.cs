using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class SprintValidationService : ISprintValidationService
    {
        private readonly EventraDBContext _dbContext;

        public SprintValidationService(EventraDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> IsTitleUniqueAsync(Guid? projectId, Guid sprintId, string title, CancellationToken cancellationToken)
        {
            return !await _dbContext.Sprints
                .AnyAsync(s => s.ProjectId == projectId
                            && s.Id != sprintId
                            && s.Title == title, cancellationToken);
        }

        public async Task<bool> HasNoDateOverlapAsync(Guid? projectId, Guid sprintId, DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken)
        {
            return !await _dbContext.Sprints
                .AnyAsync(s => s.ProjectId == projectId
                            && s.Id != sprintId
                            && startDate <= s.EndDate
                            && s.StartDate <= endDate, cancellationToken);
        }
    }
}
