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
using Loyalty.Domain.Contracts;
using MediatR;

namespace Loyalty.Application.Venue
{
    public class LoyaltyVenueImageAppService : BaseAppService
    {
        private readonly IMapper mapper;

        public LoyaltyVenueImageAppService(IMediator mediator, IMapper mapper)
            : base(mediator)
        {
            this.mapper = mapper;
        }

        public async Task<List<VenueImage>> GetImages(HttpRequestMessage request, long venueId, int index)
        {
            var content = await request.Content.ReadAsMultipartAsync();
            var images = new List<VenueImage>();

            foreach (HttpContent file in content.Contents)
            {
                var byteImage = await file.ReadAsByteArrayAsync();
                var venueImage = new VenueImage
                {
                    VenueId = venueId,
                    Image = byteImage,
                    Index = index
                };

                new VenueImageValidator()
                    .ValidateAndThrow(venueImage);

                images.Add(venueImage);            
            }
            return images;
        }

        public async Task<VenueFullViewModel> GetOriginalForVenue(long venueId)
        {
            throw new NotImplementedException();
        }

        public async Task<VenueFullViewModel> GetMediumForVenue(long venueId)
        {
           throw new NotImplementedException();
        }

        public async Task<VenueFullViewModel> SaveMediumForVenue(long venueId)
        {
            throw new NotImplementedException();
        }

        public async Task<VenueFullViewModel> SaveOriginalForVenue(long venueId)
        {
            throw new NotImplementedException();
        }
    }
}
