using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Loyalty.Application.ViewModels.Validators;
using Loyalty.Application.ViewModels.Venue;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Domain.Handlers.Queries.Queries.Venue;
using MediatR;

namespace Loyalty.Application.Venue
{
    public class LoyaltyVenueAppService : BaseAppService
    {
        private readonly IMapper mapper;

        public LoyaltyVenueAppService(IMediator mediator, IMapper mapper)
            : base(mediator)
        {
            this.mapper = mapper;
        }

        public async Task<VenueViewModel> Get(long id)
        {
            var result = await Mediator.Send(new GetVenueByIdQuery
            {
                Id = id
            });

            return mapper.Map<VenueViewModel>(result);
        }

        public async Task<List<VenueViewModel>> Get(string userId)
        {
            var result = await Mediator.Send(new GetVenuesQuery
            {
                UserId = userId
            });

            return mapper.Map<List<VenueViewModel>>(result.Venues);
        }

        public async Task<List<VenueViewModel>> GetByUser(string userGuid)
        {
            var query = new GetVenuesByUserIdQuery
            {
                UserId = userGuid
            };

            var result = await Mediator.Send(query);
            return mapper.Map<List<VenueViewModel>>(result.Venues);
        }

        public async Task<ICommandResult> Create(VenueViewModel model, ClaimsPrincipal principal)
        {
            new VenueValidator().ValidateAndThrow(model);

            var command = mapper.Map<CreateVenueCommand>(model);
            command.OwnerId = principal.GetUserId();
            command.OwnerPhone = principal.GetPhone();
            command.OwnerName = principal.GetName();
            command.OwnerSurname = principal.GetSurname();

            return await Mediator.Send(command);
        }

        public async Task<ICommandResult> Update(VenueViewModel model)
        {
            new VenueValidator().ValidateAndThrow(model);

            var command = mapper.Map<UpdateVenueCommand>(model);
            var commandResult = await Mediator.Send(command);
            return commandResult;
        }

        public async Task<ICommandResult> Patch(long venueId, List<string> imageUrls)
        {
            var commandResult = await Mediator.Send(new PatchVenueImagesCommand
            {
                Id = venueId,
                Images = imageUrls
            });

            return commandResult;
        }

        public async Task<ICommandResult> Patch(long venueId, string logo)
        {
            var commandResult = await Mediator.Send(new PatchVenueLogoCommand
            {
                Id = venueId,
                Logo = logo
            });

            return commandResult;
        }

        public async Task<ICommandResult> Archive(long id, string userId)
        {
            var command = new ArchiveVenueCommand
            {
                Id = id,
                OwnerId = userId
            };

            var commandResult = await Mediator.Send(command);
            return commandResult;
        }

        public async Task<ICommandResult> Approve(long id)
        {
            var command = new ApproveVenuePatchCommand()
            {
                Id = id
            };

            var commandResult = await Mediator.Send(command);
            return commandResult;
        }
    }
}