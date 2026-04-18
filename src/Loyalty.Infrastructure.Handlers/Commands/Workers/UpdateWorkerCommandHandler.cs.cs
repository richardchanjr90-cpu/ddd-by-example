using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Notifications.Workers;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;
using Loyalty.Shared.Contracts.Enums;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Infrastructure.Handlers.Commands.Workers
{
    public class UpdateWorkerCommandHandler
        : IRequestHandler<UpdateWorkerCommand, ICommandResult>
    {
        private readonly IVenueRepository venueRepository;
        private readonly IWorkerRepository workerRepository;

        public UpdateWorkerCommandHandler(
            IVenueRepository venueRepository,
            IWorkerRepository workerRepository)
        {
            this.venueRepository = venueRepository;
            this.workerRepository = workerRepository;
        }

        public async Task<ICommandResult> Handle(UpdateWorkerCommand request, CancellationToken cancellationToken)
        {
            var worker = await workerRepository.GetAsync(request.Id, cancellationToken);

            if (worker == null)
            {
                throw new ArgumentNullException(nameof(worker));
            }

            worker.Update(
                request.WorkerId,
                request.Name, 
                request.LastName, 
                request.VenueId, 
                request.PositionName, 
                request.Role);

            workerRepository.Update(worker); 

            var commandResult = new CommandResult
            {
                Success = await workerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken),
                Result = worker.Id
            };

            return commandResult;
        }
    }
}