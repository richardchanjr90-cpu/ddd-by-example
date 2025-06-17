using System.Threading;
using System.Threading.Tasks;
using Loyalty.Data.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.Venue;
using Loyalty.Domain.Handlers.Queries.Queries.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;

namespace Loyalty.Domain.Handlers.Queries.Venue
{
    public class GetVenuesByUserIdQueryHandler : BaseHandler, IGetVenuesByUserIdQueryHandler
    {
        public GetVenuesByUserIdQueryHandler(ILoyaltyDbContext context) : base(context)
        {
        }

        public Task<GetVenuesByUserIdQueryResult> Handle(GetVenuesByUserIdQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}