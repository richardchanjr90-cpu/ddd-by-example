using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Core.Entities.Aggregates.Workers;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.Handlers.Commands.Workers
{
    public class CreateWorkerCommandHandler
        : IRequestHandler<CreateWorkerCommand, ICommandResult>
    {
        private readonly IWorkerRepository workerRepository;
        private readonly IHttpContextAccessor accessor;
        private readonly IMediator mediator;

        public CreateWorkerCommandHandler(IWorkerRepository workerRepository, IHttpContextAccessor accessor, IMediator mediator)
        {
            this.workerRepository = workerRepository;
            this.accessor = accessor;
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(CreateWorkerCommand request, CancellationToken cancellationToken)
        {
            var phone = accessor.HttpContext.User
                .Claims
                .First(x => x.Type == ClaimTypes.MobilePhone).Value;

            var userId = accessor.HttpContext.User
                .Claims
                .First(x => x.Type == ClaimTypes.NameIdentifier).Value;

            var worker = await workerRepository.GetByPhoneAsync(request.Phone, cancellationToken);

            if (worker != null)
            {
                if (String.IsNullOrEmpty(worker.WorkerId))
                {
                    await SetupInvitedWorkerAsync(worker, userId, request);
                }
            }
            else
            {
                var createWorkerCommand = new CreateWorkerWithoutVenueCommand
                {
                    WorkerId = userId,
                    Name = request.Name,
                    LastName = request.Surname,
                    Phone = phone
                };

                return await mediator.Send(createWorkerCommand, cancellationToken);
            }

            var commandResult = new CommandResult
            {
                Success = await workerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken),
                Result = worker?.Id
            };

            return commandResult;
        }

        private async Task<ICommandResult> SetupInvitedWorkerAsync(
            Worker model,
            string userId,
            CreateWorkerCommand request)
        {
            void SetupVenueIdClaimsToHaveAccessToVenue(Worker worker)
            {
                foreach (var venue in worker.VenueRoles)
                {
                    accessor.HttpContext.User.AddVenues(venue.VenueId);
                }
            }

            var workerModel = new UpdateWorkerCommand
            {
                WorkerId = userId,
                Name = model.Name,
                LastName = model.LastName,
                Id = model.Id,
                PositionName = request.PositionName,
                Role = venueWorker.Role,
                VenueId = venueWorker.VenueId,
                Phone = model.Phone
            };

            SetupVenueIdClaimsToHaveAccessToVenue(model);

            return await mediator.Send(workerModel);
        }
    }
}
