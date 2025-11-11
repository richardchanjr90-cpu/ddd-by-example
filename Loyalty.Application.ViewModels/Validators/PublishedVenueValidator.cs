using FluentValidation;
using Loyalty.Application.ViewModels.Venue;

namespace Loyalty.Application.ViewModels.Validators
{
    public class PublishedVenueValidator : AbstractValidator<VenueViewModel>
    {
        public PublishedVenueValidator()
        {
            RuleFor(x => x.CategoryType)
                .GreaterThan(0);

            RuleFor(x => x.Location)
                .NotNull()
                .SetValidator(new LocationValidator());

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(2000);
        }
    }
}
