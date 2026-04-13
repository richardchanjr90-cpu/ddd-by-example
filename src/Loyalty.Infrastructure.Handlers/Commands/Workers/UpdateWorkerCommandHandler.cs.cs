using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Notifications.Workers;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Loyalty.Shared.Contracts.Enums;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.Handlers.Commands.Workers
{
    public class UpdateWorkerCommandHandler
        : BaseHandler, IRequestHandler<UpdateWorkerCommand, ICommandResult>
    {
        private readonly IVenueRepository venueRepository;
        private readonly IWorkerRepository workerRepository;
        private readonly IMediator mediator;

        public UpdateWorkerCommandHandler(
            IVenueRepository venueRepository,
            IWorkerRepository workerRepository,
            ILoyaltyTenantDbContext context, 
            IHttpContextAccessor accessor, 
            IMediator mediator)
            : base(context, accessor)
        {
            this.venueRepository = venueRepository;
            this.workerRepository = workerRepository;
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(UpdateWorkerCommand request, CancellationToken cancellationToken)
        {
            var worker = await workerRepository.GetAsync(request.Id, cancellationToken);

            if (worker == null)
            {
                throw new ArgumentNullException(nameof(worker));
            }

            if (request.Role == VenueUserRole.Owner)
            {
                throw new LoyaltyValidationException("Impossible to create a second owner.", ErrorCode.SECOND_OWNER_NOT_ALLOWED);
            }

            worker.Update(request.Name, request.LastName);

            var venueWorker = worker.Venues.Single(x => x.VenueId == request.VenueId);
            venueWorker.PositionName = request.PositionName;
            venueWorker.Role = request.Role;

            var commandResult = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = worker.Id
            };

            if (commandResult.Success)
            {
                await mediator.Publish(
                    new UpdatedWorkerNotification
                    {
                       WorkerId = worker.WorkerId,
                       LastName = worker.LastName,
                       Name = worker.Name,
                       PhotoUri = worker.PhotoUri,
                       Role = venueWorker.Role,
                       VenueId = request.VenueId
                    },
                    cancellationToken);
            }

            return commandResult;
        }
    }
}