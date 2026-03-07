using Application.Common.Exceptions;
using Application.Common.Interfaces.Contexts;
using Domain.Entities.Issues;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.IssueActions.Commands.AssignUser
{
    public class AssignUserCommandHandler(IIssueRepository issueRepository, IUserContext userContext) : IRequestHandler<AssignUserCommand, Guid>
    {
        private readonly IIssueRepository _issueRepository = issueRepository;
        private readonly IUserContext _userContext = userContext;
        public async Task<Guid> Handle(AssignUserCommand command, CancellationToken ct)
        {
            var issue = await _issueRepository.GetIssueByIdAsync(command.IssueId, ct) ?? throw new NotFoundException(nameof(Issue), command.IssueId);
            issue.AssignAssignee(command.UserId, _userContext.UserId);
            await _issueRepository.UpdateIssueAsync(issue, ct);
            return issue.Id;
        }
    }
}
