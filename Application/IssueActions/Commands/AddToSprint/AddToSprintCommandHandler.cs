using Application.Common.Exceptions;
using Application.Common.ResponseDtos;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IssueActions.Commands.AddToSprint
{
    public class AddToSprintCommandHandler(IIssueRepository issueRepository, ISprintRepository sprintRepository) : IRequestHandler<AddToSprintCommand, Unit>
    {
        private readonly IIssueRepository _issueRepository = issueRepository;
        private readonly ISprintRepository _sprintRepository = sprintRepository;

        public async Task<Unit> Handle(AddToSprintCommand command, CancellationToken ct)
        {
            var issue = await _issueRepository.GetIssueByIdAsync(command.IssueId, ct) ?? throw new NotFoundException(nameof(Issue), command.IssueId);

            var sprint = await _sprintRepository.GetSprintByIdAsync(command.SprintId, ct) ?? throw new NotFoundException(nameof(Sprint), command.SprintId);

            issue.AssignToSprint(command.SprintId, sprint);
            await _issueRepository.UpdateIssueAsync(issue, ct);
            return Unit.Value;

        }
    }
}
