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
                createdById: command.CreatedById,
                parentIssueId: command.ParentIssueId,
                sprintId: command.SprintId,
                assigneeId: command.AssigneeId
            );

            // 3. Persist (events dispatched later by DbContext)
            await _issueRepository.CreateIssueAsync(issue, ct);

            return issue.Id;

        }
    }
}
