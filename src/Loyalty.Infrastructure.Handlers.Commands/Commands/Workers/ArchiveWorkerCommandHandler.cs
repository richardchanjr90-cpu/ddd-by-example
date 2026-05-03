using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.Handlers.Commands.Commands.Workers
{
    public class ArchiveWorkerCommandHandler
        : IRequestHandler<ArchiveWorkerCommand, ICommandResult>
    {
        private readonly IWorkerRepository workerRepository;

        public ArchiveWorkerCommandHandler(IWorkerRepository workerRepository, IHttpContextAccessor accessor)
        {
            this.workerRepository = workerRepository;
        }

        public async Task<ICommandResult> Handle(ArchiveWorkerCommand request, CancellationToken cancellationToken)
        {
            var worker = await workerRepository.GetAsync(request.Id, cancellationToken);

            var role = worker?.RemoveFromVenue(request.VenueId);
            workerRepository?.Remove(role); 

            var commandResult = new CommandResult
            {
                Success = await workerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken),
                Result = worker?.Id
            };

            return commandResult;
        }
    }
}