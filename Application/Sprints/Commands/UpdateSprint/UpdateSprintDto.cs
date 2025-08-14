using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Sprints.Commands.UpdateSprint
{
    public record UpdateSprintDto(
        string? Title,
        string? Goal,
        DateTime? StartDate,
        DateTime? EndDate,
        Guid? ProjectId,
        SprintStatus? Status 
    );
}
