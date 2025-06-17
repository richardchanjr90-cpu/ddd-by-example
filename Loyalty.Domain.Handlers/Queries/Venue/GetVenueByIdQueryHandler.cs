using System.Threading;
using System.Threading.Tasks;
using Loyalty.Data.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.Venue;
using Loyalty.Domain.Handlers.Queries.Queries.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;

namespace Loyalty.Domain.Handlers.Queries.Venue
{
    public class GetVenueByIdQueryHandler : BaseHandler, IGetVenueByIdQueryHandler
    {
        public GetVenueByIdQueryHandler(ILoyaltyDbContext context) : base(context)
        {
        }

        public Task<GetVenueByIdQueryResult> Handle(GetVenueByIdQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}