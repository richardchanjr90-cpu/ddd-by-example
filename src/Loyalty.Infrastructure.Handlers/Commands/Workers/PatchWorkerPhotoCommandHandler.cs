using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Infrastructure.Handlers.Commands.Workers
{
    public class PatchWorkerPhotoCommandHandler
        : IRequestHandler<PatchWorkerPhotoCommand, ICommandResult>
    {
        private readonly IWorkerRepository workerRepository;

        public PatchWorkerPhotoCommandHandler(IWorkerRepository workerRepository)
        {
            this.workerRepository = workerRepository;
        }

        public async Task<ICommandResult> Handle(PatchWorkerPhotoCommand request, CancellationToken cancellationToken)
        {
            var worker = await workerRepository
                .GetByUidAsync(request.UserId, cancellationToken);

            worker.ChangeProfilePhoto(request.PhotoUri); 
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