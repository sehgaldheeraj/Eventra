using Application.Projects.ReadDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.QueryRepositories
{
    public interface IProjectQueryRepository
    {
        Task<List<ProjectSummary>> GetProjectsSummaryAsync();
        Task<ProjectOverview?> GetProjectOverviewAsync(Guid id);
        Task<bool> ProjectExistsAsync(Guid id, CancellationToken ct);
    }
}