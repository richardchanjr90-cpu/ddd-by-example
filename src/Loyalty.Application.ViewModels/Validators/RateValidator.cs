using FluentValidation;
using Loyalty.Application.ViewModels.Rate;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Application.ViewModels.Validators
{
    public class RateValidator : AbstractValidator<RateViewModel>
    {
        public RateValidator()
        {
            RuleFor(x => x.Rate)
                .GreaterThan(0)
                .LessThanOrEqualTo(5);

            RuleFor(x => x.Comment)
                .NotEmpty()
                .When(x=> x.Rate == (int)OrderVenueRate.Star);
        }
    }
}
