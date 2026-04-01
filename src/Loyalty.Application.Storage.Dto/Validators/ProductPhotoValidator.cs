using System;
using FluentValidation;
using Loyalty.Application.Storage.Dto.Validators.Interface;
using Loyalty.Common.Shared.Settings;

namespace Loyalty.Application.Storage.Dto.Validators
{
    public class ProductPhotoValidator : AbstractValidator<byte[]>
    {
        private readonly ImageSettings settings;
        private readonly ImageValidator validator;

        public ProductPhotoValidator(ImageSettings settings, ImageValidator validator)
        {
            this.settings = settings;
            this.validator = validator;

            RuleFor(x => x)
                .NotNull()
                .Must(x => x.Length > 1024)
                .WithMessage("Image should be loaded.");

            RuleFor(x => x)
                .Must(x=> validator.SizeIsLessThan1MbPlusSmallOverhead(x ,settings.MaxImageSizeInBytes))
                .WithMessage("Image must be up to 1 MB.")
                .Must(validator.IsImageValid)
                .WithMessage("Image must be in PNG or JPG format.");

            RuleFor(x => x)
                .Must(ValidateWidthAndHeight)
                .WithMessage("Image must be between 400x400 and 1200x1200px.");
        }

        private bool ValidateWidthAndHeight(byte[] arrayImage)
        {
            return validator.ValidateWidthAndHeight(arrayImage,
                settings.MaxProductImageWidth,
                settings.MaxProductImageHeight,
                settings.MinProductImageWidth,
                settings.MinProductImageHeight);
        }
    }
}