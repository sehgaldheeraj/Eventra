using FluentValidation;
using Application.Common.Interfaces;

namespace Application.Sprints.Commands.UpdateSprint
{
    public class UpdateSprintCommandValidator : AbstractValidator<UpdateSprintCommand>
    {
        private readonly ISprintValidationService _sprintValidationService;

        public UpdateSprintCommandValidator(ISprintValidationService sprintValidationService)
        {
            _sprintValidationService = sprintValidationService;

            // Title rules
            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("Sprint title must be unique in the project.")
                .When(v => !string.IsNullOrWhiteSpace(v.Title)); // only validate if Title is provided

            // StartDate rules
            RuleFor(v => v.StartDate)
                .NotEmpty().WithMessage("Start date is required.")
                .When(v => v.StartDate != default); // only when user sends StartDate

            // EndDate rules
            RuleFor(v => v.EndDate)
                .NotEmpty().WithMessage("End date is required.")
                .GreaterThan(v => v.StartDate).WithMessage("End date must be after start date.")
                .When(v => v.EndDate != default && v.StartDate != default);

            // Date overlap rule
            RuleFor(v => v)
                .MustAsync(NotOverlapWithOtherSprints)
                .When(v => v.StartDate != default && v.EndDate != default)
                .WithMessage("Sprint dates overlap with another sprint in this project.");

            // Status rules (example)
            RuleFor(v => v.Status)
                .IsInEnum()
                .WithMessage("Invalid sprint status.");
        }

        private Task<bool> BeUniqueTitle(UpdateSprintCommand model, string title, CancellationToken cancellationToken)
        {
            return _sprintValidationService.IsTitleUniqueAsync(model.ProjectId, model.Id, title, cancellationToken);
        }

        private Task<bool> NotOverlapWithOtherSprints(UpdateSprintCommand model, CancellationToken cancellationToken)
        {
            return _sprintValidationService.HasNoDateOverlapAsync(
                model.ProjectId,
                model.Id,
                model.StartDate,
                model.EndDate,
                cancellationToken);
        }
    }
}
