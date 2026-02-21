using System;
using FluentValidation;
using Loyalty.Application.ViewModels.Signup;

namespace Loyalty.Application.ViewModels.Validators
{
    public class SignupViewModelValidator: AbstractValidator<SignupViewModel>
    {
        public SignupViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Surname)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.City)
                .MaximumLength(100);
        }
    }
}
