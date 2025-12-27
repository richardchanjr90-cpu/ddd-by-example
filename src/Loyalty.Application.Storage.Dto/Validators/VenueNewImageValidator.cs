using System;
using System.IO;
using System.Net.Mime;
using FluentValidation;
using Loyalty.Common.Shared.Settings;
using SkiaSharp;

namespace Loyalty.Application.Storage.Dto.Validators
{
    public class VenueNewImageValidator : AbstractValidator<VenueNewBlobImageDto>
    {
        private readonly ImageSettings settings;

        public VenueNewImageValidator(ImageSettings settings)
        {
            this.settings = settings;

            RuleFor(x => x.Image)
                .NotNull()
                .Must(x => x.Length > 1024)
                .WithMessage("Image should be loaded.");

            RuleFor(x => x.VenueId)
                .GreaterThan(0);

            RuleFor(x => x.Image)
                .Must(SizeIsLessThan1MbPlusSmallOverhead)
                .WithMessage("Image must be up to 1 MB.")
                .Must(IsImageValid)
                .WithMessage("Image must be in PNG or JPG format.");

            RuleFor(x => x.Image)
                .Must(ValidateWidthAndHeight)
                .WithMessage("Image must be between 800x600 and 2560x1440px.");
        }

        private bool IsImageValid(byte[] arrayImage)
        {
            var isValid = false;

            try
            {
                using (var inputStream = new SKManagedStream(new MemoryStream(arrayImage)))
                {
                    //
                }
                isValid = true;
            }
            catch (Exception ex)
            {
                isValid = false;
                //Just validation; 
            }

            return isValid;
        }

        private bool ValidateWidthAndHeight(byte[] arrayImage)
        {
            bool isValid;
            try
            {
                using (var imageStream = new SKManagedStream(new MemoryStream(arrayImage)))
                {
                    using (var image = SKBitmap.Decode(imageStream))
                    {
                        isValid = image.Width <= settings.MaxGalleryImageWidth;
                        isValid = isValid && image.Height <= settings.MaxGalleryImageHeight;
                        isValid = isValid && image.Height >= settings.MinGalleryImageHeight;
                        isValid = isValid && image.Width >= settings.MinGalleryImageWidth;
                    }
                }
            }
            catch (Exception)
            {
                isValid = false;
            }

            return isValid;
        }

        private bool SizeIsLessThan1MbPlusSmallOverhead(byte[] array)
        {
            var isValid = false;

            if (array != null) isValid = array.Length <= settings.MaxImageSizeInBytes;

            return isValid;
        }
    }
}