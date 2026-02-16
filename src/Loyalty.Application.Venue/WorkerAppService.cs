using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Loyalty.Application.ViewModels.UserProfile;
using Loyalty.Application.ViewModels.Validators;
using Loyalty.Application.ViewModels.Worker;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.UserProfile;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;
using Loyalty.Domain.Handlers.Queries.Commands.Workers.Invites;
using Loyalty.Domain.Handlers.Queries.Queries.UserProfile;
using Loyalty.Domain.Handlers.Queries.Queries.Worker;
using Loyalty.Domain.Handlers.Queries.QueryResults.Worker;
using MediatR;

namespace Loyalty.Application.Venue
{
    public class WorkerAppService : BaseAppService
    {
        private readonly IMapper mapper;

        public WorkerAppService(IMediator mediator, IMapper mapper)
            : base(mediator)
        {
            this.mapper = mapper;
        }

        public async Task<FullUserProfileViewModel> GetProfile(string userId)
        {
            var result = await Mediator.Send(new GetUserProfileByIdQuery()
            {
                UserId = userId
            });

            return mapper.Map<FullUserProfileViewModel>(result);
        }

        public async Task<List<WorkerViewModel>> Get(string userId)
        {
            var result = await Mediator.Send(new GetWorkersByUserIdQuery
            {
                UserId = userId
            });

            return mapper.Map<List<WorkerViewModel>>(result.Result);
        }

        public async Task<ICommandResult> Invite(InviteViewModel model)
        {
            new WorkerInviteValidator().ValidateAndThrow(model);
            var command = mapper.Map<CreateInviteCommand>(model);

            return await Mediator.Send(command);
        }

        public async Task<ICommandResult> UpdateInvited(InviteViewModel model)
        {
            new WorkerInviteValidator().ValidateAndThrow(model);
            var command = mapper.Map<UpdateInviteCommand>(model);

            var commandResult = await Mediator.Send(command);
            return commandResult;
        }

        public async Task<ICommandResult> CompleteSignup(WorkerViewModel model)
        {
            new WorkerUpdateValidator().ValidateAndThrow(model);

            var command = mapper.Map<UpdateWorkerCommand>(model);

            var commandResult = await Mediator.Send(command);
            return commandResult;
        }

        public async Task<ICommandResult> UpdateProfile(UserProfileViewModel model, string userId)
        {
            new UserProfileUpdateValidator().ValidateAndThrow(model);

            var command = mapper.Map<UpdateUserProfileCommand>(model);
            command.WorkerId = userId;

            var commandResult = await Mediator.Send(command);
            return commandResult;
        }

        public async Task<ICommandResult> Archive(long id, string userId)
        {
            var command = new ArchiveWorkerCommand
            {
                Id = id,
                UserId = userId
            };

            var commandResult = await Mediator.Send(command);
            return commandResult;
        }

        public async Task<ICommandResult> ArchiveById(string uid, string userId)
        {
            var command = new ArchiveWorkerByUidCommand()
            {
                WorkerId = uid,
                UserId = userId
            };

            var commandResult = await Mediator.Send(command);
            return commandResult;
        }

        public async Task<GetInviteByPhoneQueryResult> GetByPhone(string phone)
        {
            var result = await Mediator.Send(new GetWorkerByPhoneQuery
            {
                Phone = phone
            });

            return result;
        }

        public async Task<ICommandResult> PatchPhoto(string logo, string userId)
        {
            var result = await Mediator.Send(new PatchWorkerPhotoCommand
            {
                UserId = userId,
                PhotoUri = logo
            });

            return result;
        }
    }
}