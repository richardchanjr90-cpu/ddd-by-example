using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Settings;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.Venues;
using Loyalty.Domain.Handlers.Queries.Queries.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using Loyalty.Infrastructure.Handlers.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Remotion.Linq.Clauses;

namespace Loyalty.Infrastructure.Handlers.Queries.Venues
{
    public class GetVenuesQueryHandler : BaseHandler, IGetVenuesQueryHandler
    {
        public GetVenuesQueryHandler(ILoyaltyDbContext context, IOptions<DbSettings> settings)
            : base(context)
        {
        }

        public async Task<GetVenuesQueryResult> Handle(GetVenuesQuery request, CancellationToken cancellationToken)
        {
            var venues = await (from v in Context.Venues
                join w in Context.Workers on v.Id equals w.VenueId
                where w.WorkerId == request.UserId select v).ToListAsync(cancellationToken);

            return new GetVenuesQueryResult
            {
                Venues = venues?.ToResults()
            };
        }
    }
}