using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Shared.Settings;
using Loyalty.Data.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.VenueDetails;
using Loyalty.Domain.Handlers.Contracts.Queries.Venues;
using Loyalty.Domain.Handlers.Extensions;
using Loyalty.Domain.Handlers.Queries.Queries.Venue;
using Loyalty.Domain.Handlers.Queries.Queries.VenueDetails;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.VenueDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Loyalty.Domain.Handlers.Queries.VenueDetails
{
    public class GetVenueDetailsByIdQueryHandler : BaseHandler, IGetVenueDetailsByIdQueryHandler
    {
        public GetVenueDetailsByIdQueryHandler(ILoyaltyDbContext context, IOptions<DbSettings> settings)
            : base(context)
        {
        }

        public async Task<GetVenueDetailsByIdQueryResult> Handle(GetVenueDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var details = await Context.VenueDetails
                .Where(x => x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            return details?.ToResult();
        }
    }
}
