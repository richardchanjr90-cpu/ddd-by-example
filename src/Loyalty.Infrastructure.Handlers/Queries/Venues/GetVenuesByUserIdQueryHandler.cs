using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Settings;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Handlers.Contracts.Queries.Venues;
using Loyalty.Domain.Handlers.Queries.Queries.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using Loyalty.Infrastructure.Handlers.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Queries.Venues
{
    public class GetVenuesByUserIdQueryHandler : BaseHandler, IGetVenuesByUserIdQueryHandler
    {
        public GetVenuesByUserIdQueryHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<GetVenuesByUserIdQueryResult> Handle(GetVenuesByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            var result = await Context.Venues.FirstOrDefaultAsync(cancellationToken);

            return new GetVenuesByUserIdQueryResult
            {
                Venues = new List<Venue>() { result }.ToResults()
            };
        }
    }
}