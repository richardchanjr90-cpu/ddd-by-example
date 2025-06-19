using System;
using FluentValidation;

namespace Loyalty.Core.ViewModels.Validators
{
    public class GeoPositionValidator : AbstractValidator<GeoPositionViewModel>
    {
        public GeoPositionValidator()
        {
            RuleFor(x => x.Id).Must(BeAValidGuid);
            RuleFor(x => x.Latitude).GreaterThanOrEqualTo(-90).LessThanOrEqualTo(90);
            RuleFor(x => x.Longitude).GreaterThanOrEqualTo(-180).LessThanOrEqualTo(180);
            RuleFor(x => x.City).NotEmpty();
        }

        private bool BeAValidGuid(string guid)
        {
            return Guid.TryParse(guid, out Guid result) && Guid.Empty != result;
        }
    }
}
