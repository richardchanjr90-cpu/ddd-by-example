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
            var role = Principal.GetRole();
            var userId = Principal.GetUserId();

            var result = await (from w in Context.Workers
                    .Include(x => x.Venues)
                                join p in Context.VenueWorkers on w.Id equals p.WorkerId
                where w.WorkerId != userId && p.Role <= role
                     select w).ToListAsync(cancellationToken);

            return new GetWorkersByUserIdQueryResult
            {
                Result = result?.ToResults()
            };
        }
    }
}