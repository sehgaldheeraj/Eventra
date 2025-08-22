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
        private readonly ISprintRepository _sprintRepository;

        public CreateIssueCommandHandler(IIssueRepository issueRepository, IUserRepository userRepository, ISprintRepository sprintRepository)
        {
            _issueRepository = issueRepository;
            _userRepository = userRepository;
            _sprintRepository = sprintRepository;
        }

        public async Task<Guid> Handle(CreateIssueCommand command, CancellationToken cancellationToken)
        {
            var assigner = await _userRepository.GetUserByIdAsync(command.AssignerId);
            if (assigner == null) {
                throw new Exception("Assigner Not Found");
            }

            User? assignee = null;
            if (command.AssigneeId.HasValue)
                assignee = await _userRepository.GetUserByIdAsync(command.AssigneeId);

            Sprint? sprint = null;
            if(command.SprintId.HasValue)
                sprint = await _sprintRepository.GetSprintByIdAsync(command.SprintId, cancellationToken);

            var issue = IssueFactory.Create(
                command.Title,
                command.Description,
                command.AssignerId, assigner,
                command.ParentIssueId,
                command.SprintId, sprint,
                command.AssigneeId, assignee    
            );
            await _issueRepository.CreateIssueAsync(issue, cancellationToken);
            return issue.Id;

        }
    }
}
