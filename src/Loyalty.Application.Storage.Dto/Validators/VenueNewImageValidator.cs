using System;
using System.Drawing;
using System.IO;
using FluentValidation;
using Loyalty.Application.Storage.Dto.Validators.Interface;
using Loyalty.Common.Shared.Settings;

namespace Loyalty.Application.Storage.Dto.Validators
{
    public class VenueNewImageValidator : AbstractValidator<VenueNewBlobImageDto>
    {
        private readonly ImageSettings settings;
        private readonly ImageValidator validator;

        public VenueNewImageValidator(ImageSettings settings, ImageValidator validator)
        {
            this.settings = settings;
            this.validator = validator;

            RuleFor(x => x.Image)
                .NotNull()
                .Must(x => x.Length > 1024)
                .WithMessage("Image should be loaded.");

            RuleFor(x => x.VenueId)
                .GreaterThan(0);

            RuleFor(x => x.Image)
                .Must(x => validator.SizeIsLessThan1MbPlusSmallOverhead(x, settings.MaxImageSizeInBytes))
                .WithMessage("Image must be up to 1 MB.")
                .Must(validator.IsImageValid)
                .WithMessage("Image must be in PNG or JPG format.");

            RuleFor(x => x.Image)
                .Must(ValidateWidthAndHeight)
                .WithMessage("Image must be between 400x400 and 2560x1440px.");
        }
        private bool ValidateWidthAndHeight(byte[] arrayImage)
        {
            return validator.ValidateWidthAndHeight(arrayImage,
                settings.MaxGalleryImageWidth,
                settings.MaxGalleryImageHeight,
                settings.MinGalleryImageWidth,
                settings.MinGalleryImageHeight);
        }
    }
}