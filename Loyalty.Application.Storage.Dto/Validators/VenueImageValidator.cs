using System;
using FluentValidation;
using Loyalty.Common.Shared.Settings;
using SixLabors.ImageSharp;

namespace Loyalty.Application.Storage.Dto.Validators
{
    public class VenueImageValidator : AbstractValidator<VenueBlobImageDto>
    {
        private readonly VenueGalleryImageSettings settings;

        public VenueImageValidator(VenueGalleryImageSettings settings)
        {
            this.settings = settings;

            RuleFor(x => x.Image)
                .NotNull()
                .Must(x=>x.Length > 1024)
                .WithMessage("Image should be loaded.");

            RuleFor(x => x.VenueId)
                .GreaterThan(0);
                
            RuleFor(x => x.Index)
                .LessThan(10)
                .GreaterThan(-1)
                .WithMessage("You can upload up tp 10 images. From 0 to 9 indexes are used.");

            RuleFor(x => x.Image)
                .Must(SizeIsLessThan1MbPlusSmallOverhead)
                .WithMessage("Image must be up to 1 MB.")
                .Must(IsImageValid)
                .WithMessage("Image must be in PNG or JPG format.");

            RuleFor(x => x.Image)
                .Must(ValidateWidthAndHeight)
                .WithMessage("Image must be between 600x400 and 2560x1440px.");
        }

        private bool IsImageValid(Byte[] arrayImage)
        {
            var isValid = false;

            try
            {
                Image.Load(arrayImage);
                isValid = true;
            }
            catch (Exception ex)
            {
                isValid = false;
                //Just validation; 
            }

            return isValid;
        }

        private bool ValidateWidthAndHeight(Byte[] arrayImage)
        {
            var isValid = false;

            try
            {
                var image = Image.Load(arrayImage);

                isValid = image.Width <= settings.MaxGalleryImageWidth;
                isValid = isValid && image.Height <= settings.MaxGalleryImageHeight;
                isValid = isValid && image.Height >= settings.MinGalleryImageHeight;
                isValid = isValid && image.Width >= settings.MinGalleryImageWidth;
            }
            catch (Exception)
            {
                isValid = false;
            }

            return isValid;
        }

        private bool SizeIsLessThan1MbPlusSmallOverhead(Byte[] array)
        {
            var isValid = false;

            if (array != null)
            {
                isValid = array.Length <= settings.MaxImageSizeInBytes;
            }

            return isValid;
        }
    }
}
