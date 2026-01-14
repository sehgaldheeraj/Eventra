using Application.Common.Interfaces;
using Application.Common.Interfaces.QueryRepositories;
using Application.Common.Validation;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IssueActions.Commands.AddToSprint
{
    public class AddToSprintCommandValidator : AbstractValidator<AddToSprintCommand>
    {
        public AddToSprintCommandValidator(IIssueQueryRepository issueRepo, ISprintQueryRepository sprintRepo) 
        {
            RuleFor(x => x.IssueId)
                .MustExist(issueRepo.IssueExistsAsync, "Issue");
            RuleFor(x => x.SprintId)
                .MustExist(sprintRepo.SprintExists, "Sprint");
        }
    }
}
