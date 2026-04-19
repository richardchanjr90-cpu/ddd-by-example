using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Infrastructure.Handlers.Commands.Workers
{
    public class UpdateWorkerCommandHandler
        : IRequestHandler<UpdateWorkerCommand, ICommandResult>
    {
        private readonly IWorkerRepository workerRepository;

        public UpdateWorkerCommandHandler(
            IWorkerRepository workerRepository)
        {
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