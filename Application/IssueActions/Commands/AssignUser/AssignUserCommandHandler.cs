using Application.Common.Exceptions;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.IssueActions.Commands.AssignUser
{
    public class AssignUserCommandHandler(IIssueRepository issueRepository, IUserRepository userRepository) : IRequestHandler<AssignUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IIssueRepository _issueRepository = issueRepository;

        public async Task<Unit> Handle(AssignUserCommand command, CancellationToken ct)
        {
            var issue = await _issueRepository.GetIssueByIdAsync(command.IssueId, ct) ?? throw new NotFoundException(nameof(Issue), command.IssueId);

            var user = await _userRepository.GetUserByIdAsync(command.UserId) ?? throw new NotFoundException(nameof(User), command.UserId); 

            issue.AssignAssignee(command.UserId, user);
            await _issueRepository.UpdateIssueAsync(issue, ct);
            return Unit.Value;
        }
    }
}
