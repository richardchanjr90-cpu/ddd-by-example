using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities;
using Loyalty.Domain.Handlers.Queries.Queries.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Infrastructure.Handlers.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Queries.Venues
{
    public class GetVenuesByUserIdQueryHandler 
        : BaseHandler, IRequestHandler<GetVenuesForAdminQuery, GetVenuesByUserIdQueryResult>
    {
        public GetVenuesByUserIdQueryHandler(ILoyaltyDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<GetVenuesByUserIdQueryResult> Handle(GetVenuesForAdminQuery request,
            CancellationToken cancellationToken)
        {
            var result = await Context.Venues.FirstOrDefaultAsync(cancellationToken);

            var results = new List<Venue>();
            if (result != null)
            {
                results.Add(result);
            }

            return new GetVenuesByUserIdQueryResult
            {
                Venues = results.ToResults()
            };
        }
    }
}