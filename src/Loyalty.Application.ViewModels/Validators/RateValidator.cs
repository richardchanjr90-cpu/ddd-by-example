using FluentValidation;
using Loyalty.Application.ViewModels.Rate;

namespace Loyalty.Application.ViewModels.Validators
{
    public class RateValidator : AbstractValidator<RateViewModel>
    {
        public RateValidator()
        {
            RuleFor(x => x.VenueId)
                .GreaterThan(0);

            RuleFor(x => x.OrderId)
                .GreaterThan(0);

            RuleFor(x => x.Rate)
                .GreaterThan(0)
                .LessThanOrEqualTo(5);

            RuleFor(x => x.UserId)
                .NotEmpty();
        }
    }
}
