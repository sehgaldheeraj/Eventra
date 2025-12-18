using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IssueActions.Commands.UnassignUser
{
    public record UnassignUserCommand(Guid IssueId) : IRequest<Guid>;
}
