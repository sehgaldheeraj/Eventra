using Application.Common.Exceptions;
using Application.Common.Interfaces.Contexts;
using Application.IssueActions.Commands.AddToSprint;
using Domain.Entities.Issues;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IssueActions.Commands.RemoveFromSprint
{
    public class RemoveFromSprintCommandHandler(IIssueRepository issueRepository, IUserContext userContext) : IRequestHandler<RemoveFromSprintCommand, Guid>
    {
        private readonly IIssueRepository _issueRepository = issueRepository;
        private readonly IUserContext _userContext = userContext;

        public async Task<Guid> Handle(RemoveFromSprintCommand command, CancellationToken ct)
        {
            var issue = await _issueRepository.GetIssueByIdAsync(command.IssueId, ct) ?? throw new NotFoundException(nameof(Issue), command.IssueId);

            issue.UnassignFromSprint(_userContext.UserId);
            await _issueRepository.UpdateIssueAsync(issue, ct);

            return issue.Id;
        }
    }
}
