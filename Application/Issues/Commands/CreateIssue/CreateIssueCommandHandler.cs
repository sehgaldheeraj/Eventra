using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Factories;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Issues.Commands.CreateIssue
{
    public class CreateIssueCommandHandler(IIssueRepository issueRepository) : IRequestHandler<CreateIssueCommand, Guid>
    {
        private readonly IIssueRepository _issueRepository = issueRepository;

        public async Task<Guid> Handle(CreateIssueCommand command, CancellationToken cancellationToken)
        {
            var issue = IssueFactory.Create(
                command.Title,
                command.Description,
                command.AssignerId, 
                command.ProjectId,
                command.ParentIssueId, 
                command.SprintId, 
                command.AssigneeId
            );

            await _issueRepository.CreateIssueAsync(issue, cancellationToken);

            return issue.Id;

        }
    }
}
