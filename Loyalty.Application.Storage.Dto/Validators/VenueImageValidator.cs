using System;
using System.IO;
using FluentValidation;
using SixLabors.ImageSharp;

namespace Loyalty.Application.Storage.Dto.Validators
{
    public class VenueImageValidator : AbstractValidator<VenueImage>
    {
        public VenueImageValidator()
        {
            RuleFor(x => x.Image)
                .NotNull()
                .Must(x=>x.Length > 1024)
                .WithMessage("Image should be loaded.");

            RuleFor(x => x.VenueId)
                .GreaterThan(0);
                
            RuleFor(x => x.Index)
                .LessThan(9)
                .GreaterThan(-1)
                .WithMessage("You can upload up tp 10 images. From 0 to 9 indexes are used.");

            RuleFor(x => x.Image)
                .Must(SizeIsLessThan1MbPlusSmallOverhead)
                .WithMessage("Image must be up to 1 MB.")
                .Must(IsImageValid)
                .WithMessage("Image must be in PNG or JPG format.");
        }

        private static bool IsImageValid(Byte[] arrayImage)
        {
            var isValid = false;

            try
            {
                Image.Load(arrayImage);
                isValid = true;
            }
            catch (Exception ex)
            {
                //Just validation; 
            }

            return isValid;
        }

        private static bool SizeIsLessThan1MbPlusSmallOverhead(Byte[] array)
        {
            var isValid = false;

            if (array != null)
            {
                isValid = array.Length <= 1024 * 1024 + 1024;
            }

            return isValid;
        }
    }
}
