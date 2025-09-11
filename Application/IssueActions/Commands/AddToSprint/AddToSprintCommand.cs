using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.ResponseDtos;
using Domain.Entities;
using MediatR;
namespace Application.IssueActions.Commands.AddToSprint
{
    public class AddToSprintCommand : IRequest<Unit>
    {
        public Guid SprintId { get; set; }
        public Guid IssueId { get; set; }

        public AddToSprintCommand(Guid sprintId, Guid issueId)
        {
            SprintId = sprintId;
            IssueId = issueId;
        }
    }
}
