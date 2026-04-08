using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AzureExtensions.FunctionToken;
using FluentValidation;
using Loyalty.Application.ViewModels.Signup;
using Loyalty.Application.ViewModels.UserProfile;
using Loyalty.Application.ViewModels.Validators;
using Loyalty.Application.ViewModels.Worker;
using Loyalty.Common.Shared.Settings;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Firebase.Queries.Commands.User;
using Loyalty.Domain.Handlers.Firebase.Queries.Queries;
using Loyalty.Domain.Handlers.Firebase.Queries.QueryResults;
using Loyalty.Domain.Handlers.Queries.Commands.UserProfile;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;
using Loyalty.Domain.Handlers.Queries.Commands.Workers.Invites;
using Loyalty.Domain.Handlers.Queries.Queries.UserProfile;
using Loyalty.Domain.Handlers.Queries.Queries.Worker;
using Loyalty.Domain.Handlers.Queries.QueryResults.UserProfile;
using Loyalty.Domain.Handlers.Queries.QueryResults.Worker;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.Extensions.Options;

namespace Loyalty.Application.Venue
{
    public class WorkerAppService : BaseAppService
    {
        private readonly IMapper mapper;
        private readonly IOptions<ImageStorageSettings> imageStorageSettings;

        public WorkerAppService(
            IMediator mediator,
            IMapper mapper,
            IOptions<ImageStorageSettings> imageStorageSettings)
            : base(mediator)
        {
            this.mapper = mapper;
            this.imageStorageSettings = imageStorageSettings;
        }

        public async Task<GetUserProfileByIdQueryResult> GetProfile(string userId)
        {
            var result = await Mediator.Send(new GetUserProfileByIdQuery
            {
                UserId = userId
            });

            return result;
        }

        public async Task<GetWorkerByIdQueryResult> Get(long id)
        {
            var result = await Mediator.Send(new GetWorkerByIdQuery
            {
                Id = id
            });

            return result;
        }

        public async Task<List<GetWorkerByIdQueryResult>> Get(string userId)
        {
            var result = await Mediator.Send(new GetWorkersByUserIdQuery
            {
                UserId = userId
            });

            return result.Result;
        }

        public async Task<ICommandResult> Invite(InviteViewModel model)
        {
            new WorkerInviteValidator().ValidateAndThrow(model);
            var command = mapper.Map<CreateInviteCommand>(model);

            return await Mediator.Send(command);
        }

        public async Task<ICommandResult> UpdateInvited(UpdateInviteViewModel model)
        {
            new UpdateWorkerInviteValidator()
                .ValidateAndThrow(model);

            var command = mapper.Map<UpdateInviteCommand>(model);

            return await Mediator.Send(command);;
        }

        public async Task<ICommandResult> UpdateProfile(UserProfileViewModel model, FunctionTokenResult token, string userId)
        {
            new UserProfileUpdateValidator()
                .ValidateAndThrow(model);

            var command = mapper.Map<UpdateUserProfileCommand>(model);
            command.WorkerId = userId;
            var commandResult = await Mediator.Send(command);

            ICommandResult result2 = null;

            if (commandResult.Success)
            {
                result2 = await Mediator.Send(new UpdateFirebaseTokenCommand
                {
                    Email = model.Email,
                    Surname = model.LastName,
                    Name = model.Name,
                    Token = token
                });
            }

            //todo: do it in a transaction.
            return result2 ?? commandResult;
        }

        public async Task<ICommandResult> UpdateProfile(string email, string userId)
        {
            var commandResult = await Mediator.Send(new UpdateEmailCommand()
            {
                WorkerId = userId,
                Email = email
            });

            return commandResult;
        }

        public async Task<ICommandResult> SetupEmail(PatchEmailViewModel model, string userId)
        {
            new PatchEmailValidator()
                .ValidateAndThrow(model);

            var user = await Mediator.Send(new GetCurrentUserQuery()
            {
                UserId = userId
            });

            var updateUser = await Mediator.Send(new UpdateUserEmailCommand()
            {
                CurrentEmail = user.Email,
                IsEmailVerified = user.IsEmailVerified,
                NewEmail = model.Email,
                UserId = user.UserId,
            });

            if (updateUser.Success)
            {
                var result = await Mediator.Send(new GetVerificationLinkQuery()
                {
                    Surname =  user.Surname,
                    Name =  user.Name,
                    UserId = user.UserId,
                    NewEmail = model.Email
                });

                if (!String.IsNullOrEmpty(result.Link))
                {
                    await UpdateProfile(model.Email, userId);
                }
            }

            return updateUser;
        }

        public async Task<ICommandResult> Archive(long venueId, long id, string userId)
        {
            var command = new ArchiveWorkerCommand
            {
                Id = id,
                VenueId = venueId,
                UserId = userId
            };

            var commandResult = await Mediator.Send(command);
            return commandResult;
        }

        public async Task<ICommandResult> PatchPhoto(string logo, string userId)
        {
            if (!String.IsNullOrEmpty(logo) && !String.IsNullOrEmpty(imageStorageSettings.Value.CDN) &&
                !String.IsNullOrEmpty(imageStorageSettings.Value.StorageAccountUrl))
            {
                logo = logo.Replace(imageStorageSettings.Value.StorageAccountUrl, imageStorageSettings.Value.CDN);
            }

            var result = await Mediator.Send(new PatchWorkerPhotoCommand
            {
                UserId = userId,
                PhotoUri = logo
            });

            return result;
        }
    }
}