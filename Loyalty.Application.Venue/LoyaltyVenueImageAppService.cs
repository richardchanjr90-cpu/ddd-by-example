using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Loyalty.Application.Storage.Dto;
using Loyalty.Application.Storage.Dto.Validators;
using Loyalty.Common.Shared.Settings;
using Loyalty.Domain.Contracts;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Loyalty.Application.Venue
{
    public class LoyaltyVenueImageAppService : BaseAppService
    {
        private readonly IMapper mapper;
        private readonly IOptions<VenueGalleryImageSettings> settings;

        public LoyaltyVenueImageAppService(IMediator mediator, IMapper mapper,
            IOptions<VenueGalleryImageSettings> settings)
            : base(mediator)
        {
            this.mapper = mapper;
            this.settings = settings;
        }

        public async Task<List<VenueNewBlobImageDto>> ConvertImages(HttpRequestMessage request, long venueId, Guid index)
        {
            var content = await request.Content.ReadAsMultipartAsync();

            var images = new List<VenueNewBlobImageDto>();

            foreach (var file in content.Contents)
            {
                var byteImage = await file.ReadAsByteArrayAsync();
                var venueImage = new VenueNewBlobImageDto
                {
                    VenueId = venueId,
                    Image = byteImage,
                    Index = index
                };

                new VenueNewImageValidator(settings.Value)
                    .ValidateAndThrow(venueImage);

                images.Add(venueImage);
            }

            return images;
        }

        public async Task<List<VenueNewBlobImageDto>> ConvertImages(HttpRequestMessage request, long venueId)
        {
            var content = await request.Content.ReadAsMultipartAsync();

            var images = new List<VenueNewBlobImageDto>();

            foreach (var file in content.Contents)
            {
                var byteImage = await file.ReadAsByteArrayAsync();
                var venueImage = new VenueNewBlobImageDto
                {
                    VenueId = venueId,
                    Image = byteImage,
                    Index = Guid.NewGuid()
                };

                new VenueNewImageValidator(settings.Value)
                    .ValidateAndThrow(venueImage);

                images.Add(venueImage);
            }

            return images;
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
    }
}