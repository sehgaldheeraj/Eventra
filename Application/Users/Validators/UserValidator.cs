using Domain.Entities;
using FluentValidation;

namespace Application.Users.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(150);

            RuleFor(u => u.Password)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(100);

            RuleFor(u => u.Role)
                .NotEmpty()
                .MaximumLength(20);
        }
    }
}
