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
    public class GetVenuesByUserIdQueryHandler : BaseHandler, IGetVenuesByUserIdQueryHandler
    {
        public GetVenuesByUserIdQueryHandler(ILoyaltyDbContext context, IOptions<DbSettings> settings)
            : base(context)
        {
        }

        public async Task<GetVenuesByUserIdQueryResult> Handle(GetVenuesByUserIdQuery request, CancellationToken cancellationToken)
        {
            var result = await
                (from v in Context.Venues
                    join lprog in Context.LoyaltyPrograms on v.Id equals lprog.VenueId
                    from lprod in Context.LoyaltyProducts
                    where lprog.Id == lprod.LoyaltyProgramId
                    from card in Context.Cards
                    where card.LoyaltyProductId == lprod.Id && card.UserId == request.UserId
                    select v).ToListAsync(cancellationToken);

            return new GetVenuesByUserIdQueryResult
            {
                Venues = result.ToResults()
            };
        }
    }
}