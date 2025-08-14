using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Loyalty.Core.ViewModels;
using Loyalty.Core.ViewModels.Validators;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Domain.Handlers.Queries.Queries.Venue;
using MediatR;

namespace Loyalty.Venue.Service
{
    public class LoyaltyVenueAppService : BaseAppService
    {
        private readonly IMapper mapper;

        public LoyaltyVenueAppService(IMediator mediator, IMapper mapper)
            : base(mediator)
        {
            this.mapper = mapper;
        }

        public async Task<VenueViewModel> Get(Guid id)
        {
            var result = await Mediator.Send(new GetVenueByIdQuery
            {
                ItemId = id
            });

            return mapper.Map<VenueViewModel>(result);
        }

        public async Task<List<VenueViewModel>> Get()
        {
            var result = await Mediator.Send(new GetVenuesQuery());
            return mapper.Map<List<VenueViewModel>>(result.Venues);
        }

        public async Task<List<VenueViewModel>> GetByUser(Guid userGuid)
        {
            var query = new GetVenuesByUserIdQuery
            {
                UserId = userGuid
            };

            var result = await Mediator.Send(query);
            return mapper.Map<List<VenueViewModel>>(result.Venues);
        }

        public async Task<ICommandResult> Create(VenueViewModel model)
        {
            new VenueValidator().ValidateAndThrow(model);

            var command = mapper.Map<CreateVenueCommand>(model);
            return await Mediator.Send(command);
        }

        public async Task<ICommandResult> Update(VenueViewModel model)
        {
            new VenueValidator().ValidateAndThrow(model);

            var command = mapper.Map<UpdateVenueCommand>(model);
            var commandResult = await Mediator.Send(command);
            return commandResult;
        }

        public async Task<ICommandResult> Delete(Guid id)
        {
            var command = new ArchiveVenueCommand
            {
                Id = id,
                OwnerId = Guid.NewGuid()
            };

            var commandResult = await Mediator.Send(command);
            return commandResult;
        }
    }
}
