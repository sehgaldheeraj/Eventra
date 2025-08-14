using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
namespace Application.Sprints.Commands.UpdateSprint
{
    public class UpdateSprintCommandValidator : AbstractValidator<UpdateSprintCommand>
    {
        private readonly ISprintValidationService _sprintValidationService;

        public UpdateSprintCommandValidator(ISprintValidationService sprintValidationService)
        {
            _sprintValidationService = sprintValidationService;

            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("Sprint title must be unique in the project.");

            RuleFor(v => v.StartDate)
                .NotEmpty().WithMessage("Start date is required.");

            RuleFor(v => v.EndDate)
                .NotEmpty().WithMessage("End date is required.")
                .GreaterThan(v => v.StartDate).WithMessage("End date must be after start date.");

            RuleFor(v => v)
                .MustAsync(NotOverlapWithOtherSprints)
                .WithMessage("Sprint dates overlap with another sprint in this project.");
        }

        private Task<bool> BeUniqueTitle(UpdateSprintCommand model, string title, CancellationToken cancellationToken)
        {
            return _sprintValidationService.IsTitleUniqueAsync(model.ProjectId, model.Id, title, cancellationToken);
        }

        private Task<bool> NotOverlapWithOtherSprints(UpdateSprintCommand model, CancellationToken cancellationToken)
        {
            return _sprintValidationService.HasNoDateOverlapAsync(model.ProjectId, model.Id, model.StartDate, model.EndDate, cancellationToken);
        }
    }
}
