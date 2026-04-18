using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AzureExtensions.FunctionToken;
using FluentValidation;
using Loyalty.Application.ViewModels.Signup;
using Loyalty.Application.ViewModels.Validators;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Firebase.Queries.Commands.User;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;
using Loyalty.Domain.Handlers.Queries.Queries.Worker;
using Loyalty.Domain.Handlers.Queries.QueryResults.Worker;
using Loyalty.Shared.Contracts.Enums;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Application.Venue
{
    public class SignupAppService : BaseAppService
    {
        public SignupAppService(
            IMediator mediator)
            : base(mediator)
        {
        }
        
        public async Task<ICommandResult> StartSignup(SignupViewModel model, FunctionTokenResult token)
        {
            new SignupViewModelValidator()
                .ValidateAndThrow(model);

            var phone = token.Principal
                .Claims
                .First(x => x.Type == ClaimTypes.MobilePhone).Value;

            var userId = token.Principal
                .Claims
                .First(x => x.Type == ClaimTypes.NameIdentifier).Value;

            ICommandResult result;
            ICommandResult result2 = new CommandResult();

            var worker = await GetByPhone(phone);
            GetVenueWorkerResult venueWorker = null;

            if (worker != null)
            {
                venueWorker = IsInvitedUserWithPhoneInRoleOrThrow(worker);
                result = await SetupInvitedWorker(model, token, userId, worker, venueWorker);
            }
            else
            {
                result = await SetupOwner(model, userId, phone);
            }

            var ids = worker?.Venues
                .Select(x => x.VenueId.ToString())
                .ToCommaSeparatedStringOrNull();

            var role = VenueUserRole.Owner;

            if (venueWorker != null)
            {
                role = venueWorker.Role;
            }

            if (result.Success)
            {
                result2 = await Mediator.Send(new SetupFirebaseTokenCommand
                {
                    City = model.City,
                    Surname = model.Surname,
                    Name = model.Name,
                    Token = token,
                    Role = role,
                    VenueIds = ids
                });
            }

            return new CommandResult
            {
                Success = result.Success && result2.Success
            };
        }

        private async Task<ICommandResult> SetupInvitedWorker(
            SignupViewModel model, 
            FunctionTokenResult functionTokenResult,
            string userId,
            GetInviteByPhoneQueryResult getInviteByPhoneQueryResult, 
            GetVenueWorkerResult venueWorker)
        {
            void SetupVenueIdClaimsToHaveAccessToVenue(FunctionTokenResult token, GetInviteByPhoneQueryResult worker)
            {
                foreach (var venue in worker.Venues)
                {
                    token.Principal.AddVenues(venue.VenueId);
                }
            }

            var workerModel = new UpdateWorkerCommand
            {
                WorkerId = userId,
                Name = model.Name,
                LastName = model.Surname,
                Id = getInviteByPhoneQueryResult.Id,
                PositionName = venueWorker.PositionName,
                Role = venueWorker.Role,
                VenueId = venueWorker.VenueId,
                Phone = getInviteByPhoneQueryResult.Phone
            };
            SetupVenueIdClaimsToHaveAccessToVenue(functionTokenResult, getInviteByPhoneQueryResult);
            return await CompleteSignup(workerModel);
        }

        private async Task<ICommandResult> SetupOwner(SignupViewModel model, string userId, string phone)
        {
            var createWorkerCommand = new CreateWorkerWithoutVenueCommand
            {
                WorkerId = userId,
                Name = model.Name,
                LastName = model.Surname,
                Phone = phone
            };

            return await Mediator.Send(createWorkerCommand);
        }

        private GetVenueWorkerResult IsInvitedUserWithPhoneInRoleOrThrow(GetInviteByPhoneQueryResult worker)
        {
            GetVenueWorkerResult venueWorker;
            try
            {
                venueWorker = worker.Venues.Single();
            }
            catch (Exception ex)
            {
                throw new LoyaltyValidationException("User has already signed up", ex, ErrorCode.DUPLICATED_ENTITY);
            }

            return venueWorker;
        }

        private async Task<ICommandResult> CompleteSignup(UpdateWorkerCommand model)
        {
            return await Mediator.Send(model);
        }

        private async Task<GetInviteByPhoneQueryResult> GetByPhone(string phone)
        {
            var result = await Mediator.Send(new GetWorkerByPhoneQuery
            {
                Phone = phone
            });

            return result;
        }

        private async Task<GetInviteByEmailQueryResult> GetByEmail(string email)
        {
            var result = await Mediator.Send(new GetWorkerByEmailQuery
            {
                Email = email
            });

            return result;
        }
    }
}
