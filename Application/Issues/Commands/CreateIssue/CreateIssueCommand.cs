using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Issues.Commands.CreateIssue
{
    public class CreateIssueCommand : IRequest<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? ParentIssueId { get; set; }
        public Guid? SprintId { get; set; }
        public Guid AssignerId { get; set; }
        public Guid? AssigneeId { get; set; }

    }
}
