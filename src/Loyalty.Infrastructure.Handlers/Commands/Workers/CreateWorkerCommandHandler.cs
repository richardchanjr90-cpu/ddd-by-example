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
using Loyalty.Shared.Contracts.Enums;
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

            var worker = await workerRepository.GetByPhoneAsync(phone, cancellationToken);

            if (worker != null)
            {
                if (String.IsNullOrEmpty(worker.WorkerId))
                {
                    await SetupInvitedWorkerAsync(
                        worker, 
                        request.Name, 
                        request.Surname,
                        request.City,
                        userId);
                }
            }
            else
            {
                var createWorkerCommand = new CreateWorkerWithoutVenueCommand
                {
                    WorkerId = userId,
                    Name = request.Name,
                    LastName = request.Surname,
                    Phone = phone,
                    City = request.City
                };

                return await mediator.Send(createWorkerCommand, cancellationToken);
            }

            var commandResult = new CommandResult
            {
                Success = await workerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken),
                Result = worker.Id
            };

            return commandResult;
        }

        private async Task<ICommandResult> SetupInvitedWorkerAsync(
            Worker model,
            string name,
            string surname,
            string city,
            string userId)
        {
            var workerModel = new UpdateWorkerCommand
            {
                WorkerId = userId,
                Name = name,
                LastName = surname,
                City = city,
                Id = model.Id
            };

            foreach (var venue in model.VenueRoles)
            {
                accessor.HttpContext.User.AddVenues(venue.VenueId);
            }

            return await mediator.Send(workerModel);
        }
    }
}
