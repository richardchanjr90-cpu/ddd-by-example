using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Domain.Handlers.Queries.Queries.Worker;
using Loyalty.Domain.Handlers.Queries.QueryResults.Worker;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Infrastructure.Handlers.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Queries.Workers
{
    public class GetWorkersQueryHandler : BaseHandler, IRequestHandler<GetWorkersQuery, GetWorkersQueryResult>
    {
        public GetWorkersQueryHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<GetWorkersQueryResult> Handle(GetWorkersQuery request, CancellationToken cancellationToken)
        {
            var workers = await (from lp in Context.Workers
                    .Include(x => x.Venues)
                    .ThenInclude(x => x.Worker)
                where lp.Venues.Any(x => x.VenueId == request.VenueId)
                select lp).ToListAsync(cancellationToken);

            return new GetWorkersQueryResult
            {
                Result = workers?.ToResults()
            };
        }
    }
}