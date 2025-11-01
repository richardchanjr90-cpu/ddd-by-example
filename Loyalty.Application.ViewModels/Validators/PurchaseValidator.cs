using FluentValidation;
using Loyalty.Application.ViewModels.Validators.Extensions;
using Loyalty.Application.ViewModels.Purchase;

namespace Loyalty.Application.ViewModels.Validators
{
    public class PurchaseValidator : AbstractValidator<PurchaseViewModel>
    {
        public PurchaseValidator()
        {
            RuleFor(x => x.LoyaltyProductGroupId)
                .GreaterThan(0);

            RuleFor(x => x.UserId).Must(this.BeValidGuid)
                .WithMessage("Should be a valid guid.");

            RuleFor(x => x.Value)
                .GreaterThan(0);
        }
    }
}
