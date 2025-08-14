using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ISprintValidationService
    {
        Task<bool> IsTitleUniqueAsync(Guid? projectId, Guid sprintId, string title, CancellationToken cancellationToken);
        Task<bool> HasNoDateOverlapAsync(Guid? projectId, Guid sprintId, DateTime? startDate, DateTime? endDate, CancellationToken cancellationToken);
    }
}
