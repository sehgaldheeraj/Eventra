using Application.Common.Exceptions;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IssueActions.Commands.CloseIssue
{
    public class CloseIssueCommandHandler(IIssueRepository issueRepository) : IRequestHandler<CloseIssueCommand, Unit>
    {
        private readonly IIssueRepository _issueRepository = issueRepository;

        public async Task<Unit> Handle(CloseIssueCommand command, CancellationToken ct)
        {
            var issue = await _issueRepository.GetIssueByIdAsync(command.IssueId, ct) ?? throw new NotFoundException(nameof(Issue), command.IssueId);

            issue.Close();
            await _issueRepository.UpdateIssueAsync(issue, ct);
            return Unit.Value;
        }
    }
}
