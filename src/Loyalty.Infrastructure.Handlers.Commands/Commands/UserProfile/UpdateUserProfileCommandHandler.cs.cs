using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.UserProfile;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Infrastructure.Handlers.Commands.Commands.UserProfile
{
    public class UpdateUserProfileCommandHandler
        : IRequestHandler<UpdateUserProfileCommand, ICommandResult>
    {
        private readonly IWorkerRepository workerRepository;

        public UpdateUserProfileCommandHandler(IWorkerRepository workerRepository)
        {
            this.workerRepository = workerRepository;
        }

        public async Task<ICommandResult> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var worker = await workerRepository.GetByUidAsync(request.WorkerId, cancellationToken);

            worker.UpdateProfile(request.Name, request.LastName);

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