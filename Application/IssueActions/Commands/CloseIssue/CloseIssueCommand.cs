using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IssueActions.Commands.CloseIssue
{
    public record CloseIssueCommand(Guid IssueId) : IRequest<Unit>;
}
