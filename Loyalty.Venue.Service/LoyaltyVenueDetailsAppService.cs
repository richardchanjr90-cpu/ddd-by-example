using AutoMapper;
using Loyalty.Domain.Contracts;
using MediatR;
using System;
using System.Threading.Tasks;
using FluentValidation;
using Loyalty.Core.ViewModels;
using Loyalty.Core.ViewModels.Validators;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.VenueDetails;
using Loyalty.Domain.Handlers.Queries.Queries.VenueDetails;

namespace Loyalty.Venue.Service
{
    public class LoyaltyVenueDetailsAppService: BaseAppService
    {
        private readonly IMapper mapper;

        public LoyaltyVenueDetailsAppService(IMediator mediator, IMapper mapper) 
            : base(mediator)
        {
            this.mapper = mapper;
        }

        public async Task<VenueFullViewModel> Get(long id)
        {
            var result = await Mediator.Send(new GetVenueDetailsByIdQuery
            {
                Id = id
            });

            return mapper.Map<VenueFullViewModel>(result);
        }

        public async Task<ICommandResult> Create(long venueId, VenueDetailsViewModel model)
        {
            new VenueDetailsValidator().ValidateAndThrow(model);
            var command = mapper.Map<CreateVenueDetailsCommand>(model);
            command.VenueId = venueId;

            return await Mediator.Send(command);
        }

        public async Task<ICommandResult> Update(long venueId, VenueDetailsViewModel model)
        {
            new VenueDetailsValidator().ValidateAndThrow(model);
            var command = mapper.Map<UpdateVenueDetailsCommand>(model);
            command.VenueId = venueId;

            return await Mediator.Send(command);
        }
    }
}
