using System;
using FluentValidation;
using Loyalty.Application.ViewModels.Venue;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Application.ViewModels.Validators.Venue
{
    public class CreateVenueValidator : AbstractValidator<CreateVenueViewModel>
    {
        public CreateVenueValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200); 

            RuleFor(x => x.Type)
                .Must(x => (VenueType)x != VenueType.Single)
                .When(x => x.ParentId.HasValue)
                .WithMessage("ParentId should be > 0, when VenueType.Single.");

            RuleFor(x => x.Type)
                .GreaterThanOrEqualTo((int)VenueType.Single)
                .LessThanOrEqualTo((int)VenueType.Union);

            RuleFor(x => x.Location)
                .SetValidator(new LocationValidator())
                .When(x => x.Location != null);

            RuleFor(x => x.CategoryType)
                .GreaterThan(0);

            RuleFor(x => x.VenueApprovalStatus)
                .LessThanOrEqualTo((int)VenueApprovalStatus.Published);

            RuleForEach(x => x.Phones)
                .SetValidator(new PhoneValidator())
                .When(x => x.Phones != null);

            RuleForEach(x => x.WorkingHours)
                .Must(x => !String.IsNullOrWhiteSpace(x.Day) && x.To <= 24*60)
                .When(x => x.WorkingHours != null);
        }
    }
}