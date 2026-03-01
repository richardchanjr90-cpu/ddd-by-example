using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using FluentValidation;
using Loyalty.Application.Storage.Dto;
using Loyalty.Application.Storage.Dto.Validators;
using Loyalty.Common.Shared.Settings;
using Loyalty.Domain.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage.Blob;
using SkiaSharp;

namespace Loyalty.Application.Venue
{
    public class LoyaltyVenueImageAppService : BaseAppService
    {
        private readonly IOptions<ImageSettings> settings;

        private readonly IOptions<ImageStorageSettings> imageStorageSettings;

        public LoyaltyVenueImageAppService(IMediator mediator,
            IOptions<ImageSettings> settings,
            IOptions<ImageStorageSettings> imageStorageSettings)
            : base(mediator)
        {
            this.settings = settings;
            this.imageStorageSettings = imageStorageSettings;
        }

        public async Task<VenueNewBlobImageDto> ConvertImage(HttpRequestMessage request, long venueId, Guid index)
        {
            var image = await GetImageOrNullAsync(request);

            var venueImage = new VenueNewBlobImageDto
            {
                VenueId = venueId,
                Image = image,
                Index = index
            };

            return venueImage;
        }

        public void ValidateImage(VenueNewBlobImageDto image)
        {
            new VenueNewImageValidator(settings.Value)
                .ValidateAndThrow(image);
        }

        public void ValidateLogo(VenueNewBlobImageDto image)
        {
            new VenueLogoValidator(settings.Value)
                .ValidateAndThrow(image);
        }

        public async Task<byte[]> GetImageOrNullAsync(HttpRequestMessage request)
        {
            var content = await request.Content.ReadAsMultipartAsync();
            var file = content.Contents.FirstOrDefault();

            if (file != null)
            {
                return await file.ReadAsByteArrayAsync();
            }

            return null;
        }

        public async Task<byte[]> GetImageOrNullAsync(HttpRequest request)
        {
            var content = request.Form.Files.FirstOrDefault();
            var filePath = Path.GetTempFileName();

            if (content != null && content.Length > 0)
            {
                using (var inputStream = new FileStream(filePath, FileMode.Create))
                {
                    await content.CopyToAsync(inputStream);
                    var array = new byte[inputStream.Length];
                    inputStream.Seek(0, SeekOrigin.Begin);
                    inputStream.Read(array, 0, array.Length);

                    return array;
                }
            }

            return null;
        }

        public async Task<List<string>> GetImages(CloudBlobContainer container, string prefix)
        {
            var results = new List<string>();
            var exists = await container.ExistsAsync();

            if (exists)
            {
                var operation =
                    await container.ListBlobsSegmentedAsync(prefix, true, BlobListingDetails.None, 50,
                        null, null, null);
                results = operation.Results.Select(item => item.Uri.ToString()).ToList();

                if (!String.IsNullOrEmpty(imageStorageSettings.Value.CDN) &&
                    !String.IsNullOrEmpty(imageStorageSettings.Value.StorageAccountUrl))
                {
                    results = results.Select(x =>
                        x.Replace(imageStorageSettings.Value.StorageAccountUrl, imageStorageSettings.Value.CDN)).ToList();
                }
            }

            return results;
        }

        public async Task<int> GetCount(CloudBlobContainer container)
        {
            var results = 0;
            var exists = await container.ExistsAsync();

            if (exists)
            {
                var operation =
                    await container.ListBlobsSegmentedAsync("original", true, BlobListingDetails.None, 50,
                        null, null, null);
                results = operation.Results.Count();
            }

            return results;
        }

        public async Task<CloudBlockBlob> GetBlobForImageAsync(CloudBlobContainer container, string blobName)
        {
            await container.CreateIfNotExistsAsync();
            var blob = container.GetBlockBlobReference(blobName);
            return blob;
        }

        public Stream SaveImageOfWidthToStream(Stream blobStream, byte[] image, float? width = null)
        {
            using (var inputStream = new SKManagedStream(new MemoryStream(image)))
            {
                using var original = SKBitmap.Decode(inputStream);
                float imageRatio = 1;

                if (width != null)
                {
                    imageRatio = original.Width / width.Value;
                }

                var info = new SKImageInfo((int)(original.Width / imageRatio), (int)(original.Height / imageRatio));
                var resized = original.Resize(info, SKFilterQuality.Medium);

                using (var result = SKImage.FromBitmap(resized))
                {
                    result.Encode(SKEncodedImageFormat.Jpeg, 80)
                        .SaveTo(blobStream);
                }

                blobStream.Position = 0;
            }

            return blobStream;
        }
    }
}