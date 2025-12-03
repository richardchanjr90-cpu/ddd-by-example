using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Workers;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Workers
{
    public class ArchiveWorkerCommandHandler
        : BaseHandler, IArchiveWorkerCommandHandler
    {
        public ArchiveWorkerCommandHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<ICommandResult> Handle(ArchiveWorkerCommand request, CancellationToken cancellationToken)
        {
            var worker = await Context.VenueWorkers
                .Include(x => x.Worker)
                .Where(x => x.WorkerId == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (worker != null)
            {
                Context.VenueWorkers.Remove(worker);
                worker.Worker.IsArchived = true;
            }

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = worker?.Id
            };
        }
    }
}