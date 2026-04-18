using System;
using FluentValidation;
using Loyalty.Application.ViewModels.Signup;

namespace Loyalty.Application.ViewModels.Validators
{
    public class PatchEmailValidator: AbstractValidator<PatchEmailViewModel>
    {
        public PatchEmailValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
