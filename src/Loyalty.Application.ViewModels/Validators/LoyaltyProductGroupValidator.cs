using System;
using FluentValidation;
using Loyalty.Application.ViewModels.LoyaltyProductGroup;

namespace Loyalty.Application.ViewModels.Validators
{
    public class LoyaltyProductGroupValidator : AbstractValidator<LoyaltyProductGroupViewModel>
    {
        public LoyaltyProductGroupValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(200)
                .NotEmpty();

            RuleFor(x => x.Description)
                .MaximumLength(2000);

            RuleFor(x => x.LoyaltyProgramId)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.ProductGroupId)
                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.Rules)
                .NotNull();

            RuleForEach(x => x.Rules.Rules)
                .NotEmpty()
                .WithMessage("Rule must not be empty");
        }
    }
}
