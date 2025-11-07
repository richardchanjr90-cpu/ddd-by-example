using FluentValidation;
using Loyalty.Application.ViewModels.Validators.Extensions;
using Loyalty.Application.ViewModels.Venue;
using Loyalty.Common.Shared.Enums.Contracts;

namespace Loyalty.Application.ViewModels.Validators
{
    public class VenueValidator : AbstractValidator<VenueViewModel>
    {
        public VenueValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.OwnerId).Must(this.BeValidGuid)
                .WithMessage("Should be a valid guid.");

            RuleFor(x => x.Type)
                .Must(x => (VenueType) x != VenueType.Single)
                .When(x => x.ParentId.HasValue)
                .WithMessage("ParentId should be > 0, when VenueType.Single.");

            RuleFor(x => x.Location)
                .SetValidator(new LocationValidator())
                .When(x => x.Location != null);


            RuleForEach(x => x.WorkingHours)
                .Must(x => !string.IsNullOrWhiteSpace(x.Day))
                .When(x => x.WorkingHours != null);

            RuleFor(x => x)
                .SetValidator(new PublishedVenueValidator())
                .When(x => x.IsPublished)
                .WithMessage("Venue can be published only when all fields are set.");

            RuleFor(x => x)
                .Must(x => x.IsPublished)
                .When(x => x.IsApproved)
                .WithMessage("Venue can't be accepted if it's not published.");
        }
    }
}