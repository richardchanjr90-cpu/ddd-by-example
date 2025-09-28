using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Loyalty.Application.ViewModels.Location;
using Loyalty.Application.ViewModels.LoyaltyProgram;

namespace Loyalty.Application.ViewModels.Validators
{
    public class LoyaltyProgramValidator : AbstractValidator<LoyaltyProgramViewModel>
    {
        public LoyaltyProgramValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(200)
                .NotEmpty();

            RuleFor(x => x.Description)
                .MaximumLength(4000);

            RuleFor(x => x.StartedDate)
                .GreaterThanOrEqualTo(DateTime.Parse("2019-01-01"));
        }
    }
}
