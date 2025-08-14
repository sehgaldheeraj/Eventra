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
    public class UpdateSprintCommand : IRequest<Unit>
    {
        public Guid Id { get; set; } 
        public string? Title { get; init; }
        public string? Goal { get; init; }
        public DateTime? StartDate { get; init; }
        public DateTime? EndDate { get; init; }
        public Guid? ProjectId { get; init; }
        public SprintStatus? Status { get; init; }
        public UpdateSprintCommand(Guid id, string? title, string? goal, DateTime? startDate, DateTime? endDate, Guid? projectId, SprintStatus? status)
        {
            Id = id;
            Title = title;
            Goal = goal;
            StartDate = startDate;
            EndDate = endDate;
            ProjectId = projectId;
            Status = status;
        }
    }
}

