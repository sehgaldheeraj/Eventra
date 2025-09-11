using Application.Common.Exceptions;
using Application.IssueActions.Commands.AssignUser;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IssueActions.Commands.UnassignUser
{
    public class UnassignUserCommandHandler(IIssueRepository issueRepository) : IRequestHandler<UnassignUserCommand, Unit>
    {
        private readonly IIssueRepository _issueRepository = issueRepository;

        public async Task<Unit> Handle(UnassignUserCommand command, CancellationToken ct)
        {
            var issue = await _issueRepository.GetIssueByIdAsync(command.IssueId, ct) ?? throw new NotFoundException(nameof(Issue), command.IssueId);

            issue.UnassignAssignee();
            await _issueRepository.UpdateIssueAsync(issue, ct);

            return Unit.Value;
        }
    }
}
