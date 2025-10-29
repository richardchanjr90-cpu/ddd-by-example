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
                .Must(x => (VenueType)x != VenueType.Single)
                .When(x => x.ParentId.HasValue)
                .WithMessage("ParentId should be > 0, when VenueType.Single.");

            RuleFor(x => x.Location)
                .SetValidator(new LocationValidator())
                .When(x => x.Location != null);

            RuleFor(x => x)
                .SetValidator(new PublishedVenueValidator())
                .When(x => x.IsPublished)
                .WithMessage("Venue can be published only when all fields are set.");

            RuleFor(x => x)
                .Must(x => x.IsPublished)
                .When(x => x.IsApproved)
                .WithMessage("Venue can't be accepted if it's not published.");

           //RuleFor(x => x.FullDescription)
           //     .NotEmpty()
           //     .MaximumLength(4000);

           // RuleForEach(x => x.Phones)
           //     .NotEmpty()
           //     .MinimumLength(8)
           //     .MaximumLength(20);

           // RuleForEach(x => x.WorkingHours)
           //     .NotEmpty().WithMessage("Enter working hours.")
           //     .Must(u => !u.Contains("\"")).WithMessage("Should not contain: \" ")
           //     .Length(4, 100).WithMessage("Length between 4 and 100 chars.");
        }
    }
}
