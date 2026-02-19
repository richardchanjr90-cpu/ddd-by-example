using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Loyalty.Application.ViewModels.UserProfile;
using Loyalty.Application.ViewModels.Worker;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Application.ViewModels.Validators
{
    public class UserProfileUpdateValidator : AbstractValidator<UserProfileViewModel>
    {
        public UserProfileUpdateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
