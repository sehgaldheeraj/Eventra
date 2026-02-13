using Application.Common.Interfaces;
using Domain.Entities.Issues;
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

        public async Task<Guid> Handle(CreateIssueCommand command, CancellationToken ct)
        {
            // 1. Core creation (single source of truth)
            var issue = Issue.Create(
                title: command.Title,
                description: command.Description,
                projectId: command.ProjectId,
                assignerId: command.AssignerId
            );

            // 2. Optional composition (still domain methods)
            if (command.ParentIssueId.HasValue)
            {
                issue.MakeSubIssue(command.ParentIssueId.Value);
            }

            if (command.SprintId.HasValue)
            {
                issue.AssignToSprint(command.SprintId.Value);
            }

            if (command.AssigneeId.HasValue)
            {
                issue.AssignAssignee(command.AssigneeId.Value);
            }

            // 3. Persist (events dispatched later by DbContext)
            await _issueRepository.CreateIssueAsync(issue, ct);

            return issue.Id;

        }
    }
}
