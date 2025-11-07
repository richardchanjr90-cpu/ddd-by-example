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

namespace Loyalty.Infrastructure.Handlers.Queries.Venues
{
    public class GetVenuesByUserIdQueryHandler : BaseHandler, IGetVenuesByUserIdQueryHandler
    {
        public GetVenuesByUserIdQueryHandler(ILoyaltyDbContext context, IOptions<DbSettings> settings)
            : base(context)
        {
        }

        public async Task<GetVenuesByUserIdQueryResult> Handle(GetVenuesByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            var result = await
                (from v in Context.Venues
                    join lprog in Context.LoyaltyPrograms on v.Id equals lprog.VenueId
                    join lprod in Context.LoyaltyProductGroups on lprog.Id equals lprod.LoyaltyProgramId
                    where lprog.Id == lprod.LoyaltyProgramId
                    from purchase in Context.Purchases
                    where purchase.UserId == request.UserId
                    select v).ToListAsync(cancellationToken);

            return new GetVenuesByUserIdQueryResult
            {
                Venues = result.ToResults()
            };
        }
    }
}