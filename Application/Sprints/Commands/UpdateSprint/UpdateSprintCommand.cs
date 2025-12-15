using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Domain.Entities;
namespace Application.Sprints.Commands.UpdateSprint
{
    public class UpdateSprintCommand(Guid id, string? title, string? goal, DateTime? startDate, DateTime? endDate, Guid? projectId, SprintStatus? status) : IRequest<Guid>
    {
        public Guid Id { get; set; } = id;
        public string? Title { get; init; } = title;
        public string? Goal { get; init; } = goal;
        public DateTime? StartDate { get; init; } = startDate;
        public DateTime? EndDate { get; init; } = endDate;
        public Guid? ProjectId { get; init; } = projectId;
        public SprintStatus? Status { get; init; } = status;
    }
}

