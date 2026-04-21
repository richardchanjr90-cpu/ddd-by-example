using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Core.Entities.Aggregates.Workers;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.Workers.Invites;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.Handlers.Commands.Workers.Invites
{
    public class CreateInviteCommandHandler
        : IRequestHandler<CreateInviteCommand, ICommandResult>
    {
        private readonly IWorkerRepository workerRepository;
        private readonly IHttpContextAccessor accessor;

        public CreateInviteCommandHandler(IWorkerRepository workerRepository, IHttpContextAccessor accessor)
        {
            this.workerRepository = workerRepository;
            this.accessor = accessor;
        }

        public async Task<ICommandResult> Handle(CreateInviteCommand request, CancellationToken cancellationToken)
        {
            var userRole = accessor.HttpContext.User.GetRole();
            var dbWorker = await workerRepository.GetByPhoneAsync(request.Phone, cancellationToken);

            var worker = Worker.CreateWorker(request.Phone, request.Name);

            if (dbWorker != null)
            {
                dbWorker.Invite(request.VenueId, request.Role, request.PositionName, userRole);
                workerRepository.Update(dbWorker);
                worker = dbWorker;
            }
            else
            {
                worker.Invite(request.VenueId, request.Role, request.PositionName, userRole);
                await workerRepository.AddAsync(worker);
            }

            return new CommandResult
            {
                Success = await workerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken),
                Result = worker.Id
            };
        }
    }
}