using System;
using FluentValidation;

namespace Loyalty.Core.ViewModels.Validators
{
    public class VenueValidator : AbstractValidator<VenueViewModel>
    {
        public VenueValidator()
        {
            RuleFor(x => x.Id).Must(BeAValidGuid);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.OwnerId).Must(BeAValidGuid);
            RuleFor(x => x.ParentId).Must(BeAValidGuid).When(x => !string.IsNullOrEmpty(x.ParentId));
            RuleFor(x => x.Location).NotNull().SetValidator(new GeoPositionValidator());
        }

        private bool BeAValidGuid(string guid)
        {
            return Guid.TryParse(guid, out Guid result) && Guid.Empty != result;
        }
    }
}
