using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Bogus;

namespace LoyaltyProgram.Tests.Setup.Data
{
    public class ImageFactory
    {
        public static byte[] GetImage(int width = 800, int height = 600)
        {
            try
            {
                return GetImagePrivate(width, height);
            }
            catch (Exception e)
            {
                return GetImage(width, height);
            }
        }


        private static byte[] GetImagePrivate(int width = 800, int height = 600)
        {
            using (WebClient client = new WebClient())
            {
                var faker = new Faker();
                var uri = faker.Image.PicsumUrl(width, height);

                byte[] bytes = client.DownloadData(uri);
                return bytes;
            }
        }

        public static MultipartFormDataContent GetImageContent(int width = 800, int height = 600)
        {
            var imageContent = new ByteArrayContent(GetImage(width, height));
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

            var requestContent = new MultipartFormDataContent();
            requestContent.Add(imageContent, "image", "image.jpg");

            return requestContent;
        }

        public static MultipartFormDataContent GetImageContent(byte [] bytes)
        {
            var imageContent = new ByteArrayContent(bytes);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

            var requestContent = new MultipartFormDataContent();
            requestContent.Add(imageContent, "image", "image.jpg");

            return requestContent;
        }

        public static byte[] Load(string uri)
        {
            using (WebClient client = new WebClient())
            {
                byte[] bytes = client.DownloadData(uri);
                return bytes;
            }
        }

        public static List<bool> GetHash(Bitmap bmpSource)
        {
            List<bool> lResult = new List<bool>();
            //create new image with 16x16 pixel
            Bitmap bmpMin = new Bitmap(bmpSource, new Size(16, 16));
            for (int j = 0; j < bmpMin.Height; j++)
            {
                for (int i = 0; i < bmpMin.Width; i++)
                {
                    //reduce colors to true / false                
                    lResult.Add(bmpMin.GetPixel(i, j).GetBrightness() < 0.5f);
                }
            }
            return lResult;
        }
    }
}
