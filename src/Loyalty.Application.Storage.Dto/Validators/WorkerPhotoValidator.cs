using System;
using System.IO;
using FluentValidation;
using Loyalty.Common.Shared.Settings;
using SkiaSharp;

namespace Loyalty.Application.Storage.Dto.Validators
{
    public class WorkerPhotoValidator : AbstractValidator<byte []>
    {
        private readonly ImageSettings settings;

        public WorkerPhotoValidator(ImageSettings settings)
        {
            this.settings = settings;

            RuleFor(x => x)
                .NotNull()
                .Must(x => x.Length > 1024)
                .WithMessage("Image should be loaded.");

            RuleFor(x => x)
                .Must(SizeIsLessThan1MbPlusSmallOverhead)
                .WithMessage("Image must be up to 1 MB.")
                .Must(IsImageValid)
                .WithMessage("Image must be in PNG or JPG format.");

            RuleFor(x => x)
                .Must(ValidateWidthAndHeight)
                .WithMessage("Image must be between 400x400 and 1200x1200px.");
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
                        isValid = image.Width <= settings.MaxPhotoImageWidth;
                        isValid = isValid && image.Height <= settings.MaxPhotoImageHeight;
                        isValid = isValid && image.Height >= settings.MinPhotoImageHeight;
                        isValid = isValid && image.Width >= settings.MinPhotoImageWidth;
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