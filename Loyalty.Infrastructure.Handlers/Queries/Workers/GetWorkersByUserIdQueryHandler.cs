using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.Workers;
using Loyalty.Domain.Handlers.Queries.Queries.Worker;
using Loyalty.Domain.Handlers.Queries.QueryResults.Worker;
using Loyalty.Infrastructure.Handlers.Extensions;
using Loyalty.Shared.Contracts.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Queries.Workers
{
    public class GetWorkersByUserIdQueryHandler : BaseHandler, IGetWorkersByUserIdQueryHandler
    {
        public GetWorkersByUserIdQueryHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<GetWorkersByUserIdQueryResult> Handle(GetWorkersByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            VenueUserRole role = Principal.GetRole();
            var userId = Principal.GetUserId();
            List<long> venuesIds = Principal.GetVenueIds();

            ////todo: filter by role >= current user role, depends on auth
            //var result = await (
            //        from workers in Context.Workers
            //        where workers.WorkerId == request.UserId
            //        from allWorkers in Context.Workers.Where(x => x.VenueId == workers.VenueId)
            //        select allWorkers)
            //    .Where(x => x.WorkerId != request.UserId)
            //    .ToListAsync(cancellationToken);
            var result = await Context.Venues
                    .Include(x => x.Workers)
                        .ThenInclude(x => x.Worker)
                    .AsNoTracking()
                    .Where(x => venuesIds.Contains(x.Id))
                .SelectMany(v => v.Workers.Where(wv => wv.Worker.WorkerId != userId)
                    .Select(w => w.Worker))
                .ToListAsync(cancellationToken);

            return new GetWorkersByUserIdQueryResult
            {
                Result = result?.ToResults()
            };
        }
    }
}