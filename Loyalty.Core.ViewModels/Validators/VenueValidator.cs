using System;
using FluentValidation;
using Loyalty.Core.ViewModels.Validators.Extensions;

namespace Loyalty.Core.ViewModels.Validators
{
    public class VenueValidator : AbstractValidator<VenueViewModel>
    {
        public VenueValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.OwnerId).Must(this.BeValidGuid)
                .WithMessage("Should be a valid guid");
            RuleFor(x => x.Location).NotNull().SetValidator(new LocationValidator());
        }
    }
}
