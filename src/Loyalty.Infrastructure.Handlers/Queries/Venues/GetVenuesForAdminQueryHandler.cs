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
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Infrastructure.Handlers.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Queries.Venues
{
    public class GetVenuesForAdminQueryHandler : BaseHandler, IGetVenuesByUserIdQueryHandler
    {
        public GetVenuesForAdminQueryHandler(ILoyaltyDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<GetVenuesByUserIdQueryResult> Handle(GetVenuesForAdminQuery request,
            CancellationToken cancellationToken)
        {
            var result = await Context.Venues
                .IgnoreQueryFilters()
                .ToListAsync(cancellationToken);

            var results = new List<Venue>();
            if (result != null)
            {
                results.AddRange(result);
            }

            return new GetVenuesByUserIdQueryResult
            {
                Venues = results.ToResults()
            };
        }
    }
}