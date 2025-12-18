using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Issues.Commands.UpdateIssue
{
    public record UpdateIssueCommand(Guid Id, string Title, string Description) : IRequest<Guid>;
}
