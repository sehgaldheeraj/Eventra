using Domain.Entities.Sprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISprintRepository
    {
        Task AddSprintAsync(Sprint sprint, CancellationToken ct);
        Task UpdateSprintAsync(Sprint sprint, CancellationToken ct);
        Task<Sprint?> GetSprintByIdAsync(Guid Id, CancellationToken ct);
        Task<IEnumerable<Sprint>> GetAllSprintsAsync(Guid projectId,
                                string? title,
                                SprintStatus? status,
                                DateTime? from,
                                DateTime? to,
                                CancellationToken cancellationToken
                            );
        Task DeleteSprintAsync(Sprint sprint, CancellationToken ct);
    }
}
