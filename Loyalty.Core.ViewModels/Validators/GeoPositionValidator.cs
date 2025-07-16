using FluentValidation;

namespace Loyalty.Core.ViewModels.Validators
{
    public class GeoPositionValidator : AbstractValidator<GeoPositionViewModel>
    {
        private const int MaxLongitudeAbs = 180;
        private const int MaxLatitudeAbs = 90;

        public GeoPositionValidator()
        {
            RuleFor(x => x.Latitude)
                .GreaterThanOrEqualTo(-MaxLatitudeAbs)
                .LessThanOrEqualTo(MaxLatitudeAbs);
            RuleFor(x => x.Longitude)
                .GreaterThanOrEqualTo(-MaxLongitudeAbs)
                .LessThanOrEqualTo(MaxLongitudeAbs);
            RuleFor(x => x.City).NotEmpty();
        }
    }
}
