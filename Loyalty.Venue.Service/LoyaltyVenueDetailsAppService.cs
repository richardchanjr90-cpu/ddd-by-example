using AutoMapper;
using Loyalty.Domain.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using Loyalty.Core.ViewModels;
using Loyalty.Core.ViewModels.Validators;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Domain.Handlers.Queries.Commands.VenueDetails;

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
            throw new NotImplementedException();
        }

        public async Task<ICommandResult> Create(VenueDetailsViewModel model)
        {
            new VenueDetailsValidator().ValidateAndThrow(model);

            var command = mapper.Map<CreateVenueDetailsCommand>(model);
            return await Mediator.Send(command);
        }

        public async Task<ICommandResult> Update(VenueDetailsViewModel model)
        {
            var command = mapper.Map<UpdateVenueDetailsCommand>(model);
            return await Mediator.Send(command);
        }
    }
}
