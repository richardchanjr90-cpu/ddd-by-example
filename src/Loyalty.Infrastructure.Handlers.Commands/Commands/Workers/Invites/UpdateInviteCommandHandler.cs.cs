using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.Workers.Invites;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.Handlers.Commands.Commands.Workers.Invites
{
    public class UpdateInviteCommandHandler
        : IRequestHandler<UpdateInviteCommand, ICommandResult>
    {
        private readonly IWorkerRepository workerRepository;
        private readonly IHttpContextAccessor accessor;

        public UpdateInviteCommandHandler(IWorkerRepository workerRepository, IHttpContextAccessor accessor)
        {
            this.workerRepository = workerRepository;
            this.accessor = accessor;
        }

        public async Task<ICommandResult> Handle(UpdateInviteCommand request, CancellationToken cancellationToken)
        {
            var worker = await workerRepository.GetAsync(request.Id, cancellationToken);

            var changerRole = accessor.HttpContext.User.GetRole();
            var changerId = accessor.HttpContext.User.GetUserId();

            worker.UpdateInvite(
                request.Name,
                request.Role,
                request.PositionName,
                request.VenueId,
                changerRole,
                changerId);

            workerRepository.Update(worker);

            return new CommandResult
            {
                Success = await workerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken),
                Result = worker.Id
            };
        }
    }
}
