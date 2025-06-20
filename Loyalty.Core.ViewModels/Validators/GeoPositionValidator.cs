using FluentValidation;
using Loyalty.Core.ViewModels.Validators.Extensions;

namespace Loyalty.Core.ViewModels.Validators
{
    public class GeoPositionValidator : AbstractValidator<GeoPositionViewModel>
    {
        public GeoPositionValidator()
        {
            RuleFor(x => x.Id).Must(this.BeValidGuid);
            RuleFor(x => x.Latitude).GreaterThanOrEqualTo(-90).LessThanOrEqualTo(90);
            RuleFor(x => x.Longitude).GreaterThanOrEqualTo(-180).LessThanOrEqualTo(180);
            RuleFor(x => x.City).NotEmpty();
        }
    }
}
