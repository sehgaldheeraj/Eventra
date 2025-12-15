using Application.Sprints.ReadDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ISprintQueryRepository
    {
        Task<SprintOverview?> GetSprintOverviewAsync(Guid id, CancellationToken ct);
    }
}
