using System.Threading;
using System.Threading.Tasks;
using Loyalty.Data.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.Venue;
using Loyalty.Domain.Handlers.Queries.Queries.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;

namespace Loyalty.Domain.Handlers.Queries.Venue
{
    public class GetVenuesQueryHandler : BaseHandler, IGetVenuesQueryHandler
    {
        public GetVenuesQueryHandler(ILoyaltyDbContext context) : base(context)
        {
        }

        public Task<GetVenuesQueryResult> Handle(GetVenuesQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}