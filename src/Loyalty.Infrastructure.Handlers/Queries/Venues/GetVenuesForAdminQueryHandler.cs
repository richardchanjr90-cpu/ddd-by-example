using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities;
using Loyalty.Core.Entities.Aggregates.Venues;
using Loyalty.Domain.Handlers.Queries.Queries.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Loyalty.Infrastructure.Handlers.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Queries.Venues
{
    public class GetVenuesForAdminQueryHandler 
        : BaseHandler, IRequestHandler<GetVenuesForAdminQuery, GetVenuesByUserIdQueryResult>
    {
        public GetVenuesForAdminQueryHandler(ILoyaltyDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<GetVenuesByUserIdQueryResult> Handle(
            GetVenuesForAdminQuery request,
            CancellationToken cancellationToken)
        {
            var result = await Context.Venues
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return new GetVenuesByUserIdQueryResult
            {
                Venues = result?.ToResults()
            };
        }
    }
}