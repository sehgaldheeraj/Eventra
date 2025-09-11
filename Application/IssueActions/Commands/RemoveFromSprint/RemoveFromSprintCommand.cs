using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IssueActions.Commands.RemoveFromSprint
{
    public class RemoveFromSprintCommand : IRequest<Unit>
    {
        public Guid IssueId { get; set; }
        public RemoveFromSprintCommand(Guid issueId) {
            IssueId = issueId;
        }
    }
}
