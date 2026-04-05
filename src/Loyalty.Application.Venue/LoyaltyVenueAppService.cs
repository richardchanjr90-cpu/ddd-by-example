using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Loyalty.Application.ViewModels.Validators.Venue;
using Loyalty.Application.ViewModels.Venue;
using Loyalty.Common.Shared.Settings;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Domain.Handlers.Queries.Queries.Venue;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.Extensions.Options;

namespace Loyalty.Application.Venue
{
    public class LoyaltyVenueAppService : BaseAppService
    {
        private readonly IMapper mapper;

        private readonly IOptions<ImageStorageSettings> imageStorageSettings;

        public LoyaltyVenueAppService(
            IMediator mediator, 
            IMapper mapper, 
            IOptions<ImageStorageSettings> imageStorageSettings)
            : base(mediator)
        {
            this.imageStorageSettings = imageStorageSettings;
            this.mapper = mapper;
        }

        public async Task<UpdateVenueViewModel> Get(long id)
        {
            var result = await Mediator.Send(new GetVenueByIdQuery
            {
                Id = id
            });

            return mapper.Map<UpdateVenueViewModel>(result);
        }

        public async Task<List<UpdateVenueViewModel>> Get(string userId)
        {
            var result = await Mediator.Send(new GetVenuesQuery
            {
                UserId = userId
            });

            return mapper.Map<List<UpdateVenueViewModel>>(result.Venues);
        }

        public async Task<List<UpdateVenueViewModel>> GetAllVenuesForAdmin()
        {
            var query = new GetVenuesForAdminQuery();
            var result = await Mediator.Send(query);

            return mapper.Map<List<UpdateVenueViewModel>>(result.Venues);
        }

        public async Task<ICommandResult> Create(CreateVenueViewModel model, ClaimsPrincipal principal)
        {
            new CreateVenueValidator()
                .ValidateAndThrow(model);

            var command = mapper.Map<CreateVenueCommand>(model);
            return await Mediator.Send(command);
        }

        public async Task<ICommandResult> AcceptOrders(long venueId)
        {
            var commandResult = await Mediator.Send(new PatchOrderAcceptanceCommand
            {
                Accept = true,
                VenueId = venueId
            });

            return commandResult;
        }

        public async Task<ICommandResult> DeclineOrders(long venueId)
        {
            var commandResult = await Mediator.Send(new PatchOrderAcceptanceCommand
            {
                Accept = false,
                VenueId = venueId
            });

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

        public async Task<ICommandResult> PatchLogo(long venueId, string logo, string smallLogo)
        {
            if (!String.IsNullOrEmpty(logo) && !String.IsNullOrEmpty(imageStorageSettings.Value.CDN) &&
                !String.IsNullOrEmpty(imageStorageSettings.Value.StorageAccountUrl))
            {
                logo = logo.Replace(imageStorageSettings.Value.StorageAccountUrl, imageStorageSettings.Value.CDN);
            }

            var commandResult = await Mediator.Send(new PatchVenueLogoCommand
            {
                Id = venueId,
                Logo = logo,
                SmallLogo = smallLogo
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

        public async Task<ICommandResult> Reject(long id)
        {
            var command = new RejectVenuePatchCommand()
            {
                Id = id
            };

            var commandResult = await Mediator.Send(command);
            return commandResult;
        }

        public async Task<ICommandResult> Update(UpdateVenueViewModel model)
        {
            new UpdateVenueValidator().ValidateAndThrow(model);

            var command = mapper.Map<UpdateVenueCommand>(model);
            var commandResult = await Mediator.Send(command);

            return commandResult;
        }
    }
}