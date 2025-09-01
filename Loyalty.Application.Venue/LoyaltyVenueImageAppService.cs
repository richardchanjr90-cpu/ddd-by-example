using System;
using System.Threading.Tasks;
using AutoMapper;
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
