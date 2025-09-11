using Application.Common.Exceptions;
using Application.IssueActions.Commands.AddToSprint;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IssueActions.Commands.RemoveFromSprint
{
    public class RemoveFromSprintCommandHandler : IRequestHandler<RemoveFromSprintCommand, Unit>
    {
        private readonly IIssueRepository _issueRepository;
        public RemoveFromSprintCommandHandler(IIssueRepository issueRepository)
        {
            _issueRepository = issueRepository;
        }
        public async Task<Unit> Handle(RemoveFromSprintCommand command, CancellationToken ct)
        {
            var issue = await _issueRepository.GetIssueByIdAsync(command.IssueId, ct) ?? throw new NotFoundException(nameof(Issue), command.IssueId);

            issue.UnassignFromSprint();
            await _issueRepository.UpdateIssueAsync(issue, ct);

            return Unit.Value;
        }
    }
}
