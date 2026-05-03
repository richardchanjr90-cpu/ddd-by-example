using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Aggregates.Workers;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Infrastructure.Handlers.Commands.Commands.Workers
{
    public class CreateWorkerWithoutVenueCommandHandler
        : IRequestHandler<CreateWorkerWithoutVenueCommand, ICommandResult>
    {
        private readonly IWorkerRepository workerRepository;

        public CreateWorkerWithoutVenueCommandHandler(IWorkerRepository workerRepository)
        {
            this.workerRepository = workerRepository;
        }

        public async Task<ICommandResult> Handle(CreateWorkerWithoutVenueCommand request, CancellationToken cancellationToken)
        {
            var worker = Worker.CreateOwner(
                request.WorkerId,
                request.Phone,
                request.Name,
                request.LastName,
                request.City);

            await workerRepository.AddAsync(worker);

            var commandResult = new CommandResult
            {
                Success = await workerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken),
                Result = worker.Id
            };

            return commandResult;
        }
    }
}