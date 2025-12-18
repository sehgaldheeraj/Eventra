using Application.Common.Exceptions;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Issues.Commands.UpdateIssue
{
    public class UpdateIssueCommandHandler(IIssueRepository issueRepository) : IRequestHandler<UpdateIssueCommand, Guid>
    {
        private readonly IIssueRepository _issueRepository = issueRepository;

        public async Task<Guid> Handle(UpdateIssueCommand command, CancellationToken ct)
        {
            var issue = await _issueRepository.GetIssueByIdAsync(command.Id, ct) ?? throw new NotFoundException(nameof(Issue), command.Id);

            issue.UpdateDetails(command.Title, command.Description);

            await _issueRepository.UpdateIssueAsync(issue, ct);

            return issue.Id;
        }
    }
}
