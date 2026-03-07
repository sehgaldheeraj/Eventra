using Application.Common.Interfaces;
using Application.Common.Interfaces.QueryRepositories;
using Application.Common.Validation;
using FluentValidation;
namespace Application.Issues.Commands.CreateIssue
{
    public class CreateIssueCommandValidator : AbstractValidator<CreateIssueCommand>
    {
        public CreateIssueCommandValidator(
            IUserQueryRepository userRepo,
            IProjectQueryRepository projectRepo,
            ISprintQueryRepository sprintRepo,
            IIssueQueryRepository issueRepo)
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title cannot be empty.")
                .MaximumLength(50).WithMessage("Title should be less than 50 characters");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description should be less than 500 characters");

            RuleFor(x => x.CreatedById)
                .MustExist(userRepo.UserExistsAsync, "Creator");

            RuleFor(x => x.AssigneeId)
                .MustExistIfProvided(userRepo.UserExistsAsync, "Assignee");

            RuleFor(x => x.ProjectId)
                .MustExist(projectRepo.ProjectExistsAsync, "Project");

            RuleFor(x => x.SprintId)
                .MustExistIfProvided(sprintRepo.SprintExists, "Sprint");

            RuleFor(x => x.ParentIssueId)
                .MustExistIfProvided(issueRepo.IssueExistsAsync, "Issue");
        }
    }
}