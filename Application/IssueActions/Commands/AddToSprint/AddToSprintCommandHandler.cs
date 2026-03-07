using Application.Common.Exceptions;
using Application.Common.Interfaces.Contexts;
using Application.Common.ResponseDtos;
using Domain.Entities.Issues;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IssueActions.Commands.AddToSprint
{
    public class AddToSprintCommandHandler(IIssueRepository issueRepository, IUserContext userContext) : IRequestHandler<AddToSprintCommand, Guid>
    {
        private readonly IIssueRepository _issueRepository = issueRepository;
        private readonly IUserContext _userContext = userContext;

        public async Task<Guid> Handle(AddToSprintCommand command, CancellationToken ct)
        {
            var issue = await _issueRepository.GetIssueByIdAsync(command.IssueId, ct) ?? throw new NotFoundException(nameof(Issue), command.IssueId);

            issue.AssignToSprint(command.SprintId, _userContext.UserId);
            await _issueRepository.UpdateIssueAsync(issue, ct);
            return issue.Id;
        }
    }
}
