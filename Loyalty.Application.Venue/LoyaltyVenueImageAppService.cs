using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Loyalty.Application.Storage.Dto;
using Loyalty.Application.Storage.Dto.Validators;
using Loyalty.Application.ViewModels;
using Loyalty.Common.Shared.Settings;
using Loyalty.Domain.Contracts;
using MediatR;
using Microsoft.Extensions.Options;

namespace Loyalty.Application.Venue
{
    public class LoyaltyVenueImageAppService : BaseAppService
    {
        private readonly IMapper mapper;
        private readonly IOptions<VenueGalleryImageSettings> settings;

        public LoyaltyVenueImageAppService(IMediator mediator, IMapper mapper, IOptions<VenueGalleryImageSettings> settings)
            : base(mediator)
        {
            this.mapper = mapper;
            this.settings = settings;
        }

        public async Task<List<VenueBlobImageDto>> GetImages(HttpRequestMessage request, long venueId, int index)
        {
            var content = await request.Content.ReadAsMultipartAsync();

            var images = new List<VenueBlobImageDto>();

            foreach (HttpContent file in content.Contents)
            {
                var byteImage = await file.ReadAsByteArrayAsync();
                var venueImage = new VenueBlobImageDto
                {
                    VenueId = venueId,
                    Image = byteImage,
                    Index = index
                };

                new VenueImageValidator(settings.Value)
                    .ValidateAndThrow(venueImage);

                images.Add(venueImage);            
            }
            return images;
        }
    }
}
