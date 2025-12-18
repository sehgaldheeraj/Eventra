using Application.Common.Interfaces;
using Application.Common.Validation;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Issues.Commands.UpdateIssue
{
    public class UpdateIssueCommandValidator : AbstractValidator<UpdateIssueCommand>
    {
        public UpdateIssueCommandValidator(IIssueQueryRepository issueRepo)
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title cannot be empty.")
                .MaximumLength(50).WithMessage("Title should be less than 50 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description should be less than 500 characters");
        }
    }
}
