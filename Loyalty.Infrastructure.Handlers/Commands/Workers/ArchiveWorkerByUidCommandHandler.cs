using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Workers;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Workers
{
    public class ArchiveWorkerByUidCommandHandler
        : BaseHandler, IRequestHandler<ArchiveWorkerByUidCommand, ICommandResult>
    {
        public ArchiveWorkerByUidCommandHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<ICommandResult> Handle(ArchiveWorkerByUidCommand request, CancellationToken cancellationToken)
        {
            var venueWorkers = await Context.VenueWorkers
                .Include(x => x.Worker)
                .Where(x => x.Worker.WorkerId == request.WorkerId)
                .ToListAsync(cancellationToken);

            var worker = venueWorkers.FirstOrDefault()?.Worker;

            foreach (var venueWorker in venueWorkers)
            {
                if (venueWorker != null)
                {
                    Context.VenueWorkers.Remove(venueWorker);
                    worker = venueWorker.Worker;
                }
            }

            if (worker != null)
            {
                worker.IsArchived = true;
            }

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = worker?.Id
            };
        }
    }
}