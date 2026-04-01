using System;
using System.Drawing;
using System.IO;

namespace Loyalty.Application.Storage.Dto.Validators.Interface
{
    public class ImageValidator
    {
        public bool IsImageValid(byte[] arrayImage)
        {
            var isValid = false;

            try
            {
                using var image = Image.FromStream(new MemoryStream(arrayImage));
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

        public bool ValidateWidthAndHeight(byte[] arrayImage, int maxWidth, int maxHeight, int minWidth, int minHeight)
        {
            bool isValid;
            try
            {
                using var image = Image.FromStream(new MemoryStream(arrayImage));
                {
                    isValid = image.Width <= maxWidth;
                    isValid = isValid && image.Height <= maxWidth;
                    isValid = isValid && image.Height >= minHeight;
                    isValid = isValid && image.Width >= minWidth;
                }
            }
            catch (Exception)
            {
                isValid = false;
            }

            return isValid;
        }

        public bool SizeIsLessThan1MbPlusSmallOverhead(byte[] array, int maxSize)
        {
            var isValid = false;

            if (array != null) isValid = array.Length <= maxSize;

            return isValid;
        }
    }
}
