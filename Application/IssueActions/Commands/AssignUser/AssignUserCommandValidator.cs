using Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IssueActions.Commands.AssignUser
{
    public class AssignUserCommandValidator : AbstractValidator<AssignUserCommand>
    {
        public AssignUserCommandValidator(IIssueQueryRepository issueRepo, IUserQueryRepository userRepo)
        {
            RuleFor(x => x.IssueId)
                .NotEmpty().WithMessage("IssueId cannot be empty.")
                .MustAsync(issueRepo.IssueExistsAsync);
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId cannot be empty.")
                .MustAsync(userRepo.UserExistsAsync);
        }
    }
}
