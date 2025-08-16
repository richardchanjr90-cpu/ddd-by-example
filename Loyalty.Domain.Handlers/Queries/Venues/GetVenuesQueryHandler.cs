using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Shared.Settings;
using Loyalty.Data.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.Venues;
using Loyalty.Domain.Handlers.Extensions;
using Loyalty.Domain.Handlers.Queries.Queries.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Loyalty.Domain.Handlers.Queries.Venues
{
    public class GetVenuesQueryHandler : BaseHandler, IGetVenuesQueryHandler
    {
        public GetVenuesQueryHandler(ILoyaltyDbContext context, IOptions<DbSettings> settings)
            : base(context)
        {
        }

        public async Task<GetVenuesQueryResult> Handle(GetVenuesQuery request, CancellationToken cancellationToken)
        {
            var venues = await Context.Venues
                .Include(x => x.Location)
                .ToListAsync(cancellationToken);

            return new GetVenuesQueryResult
            {
                Venues = venues.ToResults()
            };
        }
    }
}