using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISprintRepository
    {
        Task AddAsync(Sprint sprint);
        Task UpdateAsync(Sprint sprint);
        Task<Sprint?> GetByIdAsync(Guid? Id);
        Task<IEnumerable<Sprint>> GetAllAsync(Guid projectId,
                                string? title,
                                SprintStatus? status,
                                DateTime? from,
                                DateTime? to,
                                CancellationToken cancellationToken
                            );
        Task DeleteAsync(Guid id);
    }
}
