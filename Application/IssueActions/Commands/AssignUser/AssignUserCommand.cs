using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IssueActions.Commands.AssignUser
{
    public record AssignUserCommand(Guid IssueId, Guid UserId) : IRequest<Unit>; 
}
