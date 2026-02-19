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
                .MinimumLength(2)
                .NotEmpty();

            RuleFor(x => x.Description)
                .MaximumLength(4000);

            RuleFor(x => x.Description)
                .NotEmpty()
                .When(x => x.IsPublished)
                .WithMessage("You can't publish a program without a description");

            RuleFor(x => x.StartedDate)
                .GreaterThanOrEqualTo(DateTime.Today);

            RuleFor(x => x.EndedDate)
                .GreaterThanOrEqualTo(DateTime.Today.AddDays(1))
                .WithMessage("End date should be at least 1 day greater then current date.");

            RuleFor(x => x.StartedDate)
                .LessThanOrEqualTo(x=>x.EndedDate.AddDays(1))
                .WithMessage("End date should be at least 1 day greater then current date.");

            RuleFor(x => x.EndedDate)
                .LessThanOrEqualTo(DateTime.Today.AddYears(99))
                .WithMessage("You can't create a program with Finish date more than 99 years.");
        }
    }
}
