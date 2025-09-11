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
    public class CreateIssueCommandHandler: IRequestHandler<CreateIssueCommand, Guid>
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ISprintRepository _sprintRepository;

        public CreateIssueCommandHandler(IIssueRepository issueRepository, IUserRepository userRepository, IProjectRepository projectRepository, ISprintRepository sprintRepository)
        {
            _issueRepository = issueRepository;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _sprintRepository = sprintRepository;
        }

        public async Task<Guid> Handle(CreateIssueCommand command, CancellationToken cancellationToken)
        {
            // 1. Validate assigner
            var assigner = await _userRepository.GetUserByIdAsync(command.AssignerId);
            if (assigner == null)
                throw new Exception($"Assigner with Id {command.AssignerId} was not found.");

            // 2. Validate assignee (if provided)
            User? assignee = null;
            if (command.AssigneeId.HasValue)
            {
                assignee = await _userRepository.GetUserByIdAsync(command.AssigneeId);
                if (assignee == null)
                    throw new Exception($"Assignee with Id {command.AssigneeId.Value} was not found.");
            }

            // 3. Validate project (required)
            var project = await _projectRepository.GetAsync(command.ProjectId);
            if (project == null)
                throw new Exception($"Project with Id {command.ProjectId} was not found.");

            // 4. Validate sprint (optional)
            Sprint? sprint = null;
            if (command.SprintId.HasValue)
            {
                sprint = await _sprintRepository.GetSprintByIdAsync(command.SprintId.Value, cancellationToken);
                if (sprint == null)
                    throw new Exception($"Sprint with Id {command.SprintId.Value} was not found.");
            }

            // 5. Validate parent issue (optional)
            Issue? parentIssue = null;
            if (command.ParentIssueId.HasValue)
            {
                parentIssue = await _issueRepository.GetIssueByIdAsync(command.ParentIssueId.Value, cancellationToken);
                if (parentIssue == null)
                    throw new Exception($"Parent issue with Id {command.ParentIssueId.Value} was not found.");
            }

            // 6. Create and persist
            var issue = IssueFactory.Create(
                command.Title,
                command.Description,
                command.AssignerId, assigner,
                command.ProjectId, project,
                command.ParentIssueId, parentIssue,
                command.SprintId, sprint,
                command.AssigneeId, assignee
            );

            await _issueRepository.CreateIssueAsync(issue, cancellationToken);

            return issue.Id;

        }
    }
}
