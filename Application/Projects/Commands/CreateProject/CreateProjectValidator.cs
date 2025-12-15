using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Projects.Commands.CreateProject
{
    public class CreateProjectValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectValidator(IProjectRepository projectRepository) 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Project Name is required.");
            RuleFor(x => x.Description)
                .MaximumLength(100).WithMessage("Desciption is long, only 100 characters allowed.");
            RuleFor(x => x.OwnerId)
                .NotEmpty().WithMessage("Owner Id is required.")
                .NotEqual(Guid.Empty).WithMessage("Owner Id should be a valid Guid.");
        }
    }
}
