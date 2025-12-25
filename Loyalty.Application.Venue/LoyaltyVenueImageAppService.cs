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
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Loyalty.Application.Venue
{
    public class LoyaltyVenueImageAppService : BaseAppService
    {
        private readonly IOptions<ImageSettings> settings;

        public LoyaltyVenueImageAppService(IMediator mediator,
            IOptions<ImageSettings> settings)
            : base(mediator)
        {
            this.settings = settings;
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

            new VenueNewImageValidator(settings.Value)
                .ValidateAndThrow(venueImage);

            return venueImage;
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
            var imageStream = Image.Load(data: image);
            float imageRatio = 1;

            if (width != null)
            {
                imageRatio = imageStream.Width / width.Value;
            }

            imageStream.Mutate(operation: ctx => ctx.Resize(
                width: (int)(imageStream.Width / imageRatio),
                height: (int)(imageStream.Height / imageRatio)));

            imageStream.SaveAsJpeg(stream: blobStream);
            blobStream.Position = 0;

            return blobStream;
        }
    }
}