using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Loyalty.Core.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.ProductGroups;
using Loyalty.Domain.Handlers.Queries.Queries.ProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.ProductGroup;
using Loyalty.Infrastructure.Handlers.Extensions;
using Loyalty.Shared.Contracts.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Queries.ProductGroups
{
    public class GetProductGroupsByUserIdQueryHandler : BaseHandler, IGetProductGroupsByUserIdQueryHandler
    {
        public GetProductGroupsByUserIdQueryHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<GetProductGroupsByUserIdQueryResult> Handle(GetProductGroupsByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            //todo: this query is slow. profile and speed up required
            //var items = await (from worker in Context.Workers
            //    join prGroup in Context.ProductGroups.Include(x => x.Products) on worker.VenueId equals prGroup.VenueId
            //    where worker.Role >= VenueUserRole.Manager && worker.WorkerId == request.UserId
            //    select prGroup).ToListAsync(cancellationToken);
            var items = await Context.ProductGroups.Include(x => x.Products)
                .Include(x => x.OwnerVenue)
                .ThenInclude(x => x.Workers)
                .ThenInclude(x => x.Worker)
                .AsNoTracking()
                .Where(z => z.OwnerVenue.Workers.Select(x => x.Worker).Any(x => x.WorkerId == request.UserId))
                .ToListAsync(cancellationToken);

            return new GetProductGroupsByUserIdQueryResult
            {
                Result = items?.ToResults()
            };
        }
    }
}