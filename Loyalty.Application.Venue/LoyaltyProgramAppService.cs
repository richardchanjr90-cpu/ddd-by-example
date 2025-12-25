using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Loyalty.Application.ViewModels.LoyaltyProgram;
using Loyalty.Application.ViewModels.Validators;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyPrograms;
using Loyalty.Domain.Handlers.Queries.Queries.LoyaltyProgram;
using MediatR;

namespace Loyalty.Application.Venue
{
    public class LoyaltyProgramAppService : BaseAppService
    {
        private readonly IMapper mapper;

        public LoyaltyProgramAppService(IMediator mediator, IMapper mapper)
            : base(mediator)
        {
            this.mapper = mapper;
        }

        public async Task<LoyaltyProgramViewModel> Get(long id, long venueId)
        {
            var result = await Mediator.Send(new GetLoyaltyProgramByIdQuery
            {
                Id = id,
                VenueId = venueId
            });

            return mapper.Map<LoyaltyProgramViewModel>(result);
        }

        public async Task<List<LoyaltyProgramViewModel>> Get(long venueId)
        {
            var result = await Mediator.Send(new GetLoyaltyProgramsQuery {VenueId = venueId});
            return mapper.Map<List<LoyaltyProgramViewModel>>(result.Result);
        }

        public async Task<List<LoyaltyProgramViewModel>> Get(string userId)
        {
            var result = await Mediator.Send(new GetLoyaltyProgramsByUserIdQuery
            {
                UserId = userId
            });

            return mapper.Map<List<LoyaltyProgramViewModel>>(result.Result);
        }

        public async Task<ICommandResult> Create(LoyaltyProgramViewModel model, long venueId, string userId)
        {
            new LoyaltyProgramValidator().ValidateAndThrow(model);

            var command = mapper.Map<CreateLoyaltyProgramCommand>(model);
            command.VenueId = venueId;
            command.UserId = userId;

            return await Mediator.Send(command);
        }

        public async Task<ICommandResult> Update(LoyaltyProgramViewModel model, long venueId, string userId)
        {
            new LoyaltyProgramValidator().ValidateAndThrow(model);

            var command = mapper.Map<UpdateLoyaltyProgramCommand>(model);
            command.VenueId = venueId;
            command.UserId = userId;

            var commandResult = await Mediator.Send(command);
            return commandResult;
        }

        public async Task<ICommandResult> Archive(long id, string userId)
        {
            var command = new ArchiveLoyaltyProgramCommand
            {
                Id = id,
                UserId = userId
            };

            var commandResult = await Mediator.Send(command);
            return commandResult;
        }
    }
}