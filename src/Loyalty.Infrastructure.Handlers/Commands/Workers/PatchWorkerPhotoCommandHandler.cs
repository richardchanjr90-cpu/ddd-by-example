using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Notifications.Workers;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Workers
{
    public class PatchWorkerPhotoCommandHandler
        : BaseHandler, IRequestHandler<PatchWorkerPhotoCommand, ICommandResult>
    {
        private readonly IMediator mediator;

        public PatchWorkerPhotoCommandHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor, IMediator mediator)
            : base(context, accessor)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(PatchWorkerPhotoCommand request, CancellationToken cancellationToken)
        {
            var worker = await Context.Workers
                .IgnoreQueryFilters()
                .Include(x => x.Venues)
                .Where(x => x.WorkerId == request.UserId)
                .FirstOrDefaultAsync(cancellationToken);

            if (worker != null)
            {
                worker.PhotoUri = request.PhotoUri;
            }

            var commandResult = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = worker?.Id
            };

            if (commandResult.Success && worker != null)
            {
                await mediator.Publish(
                    new PatchWorkerNotification
                    {
                        WorkerId = worker.WorkerId,
                        PhotoUri = worker.PhotoUri,
                    },
                    cancellationToken);
            }
            return commandResult;
        }
    }
}