using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Sprints.Commands.CreateSprint
{
    public class CreateSprintCommandValidator :AbstractValidator<CreateSprintCommand>
    {
        public CreateSprintCommandValidator() { 
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title cannot be empty.");
            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate)
                .WithMessage("End date must be after start date.");
        }   
    }
}
