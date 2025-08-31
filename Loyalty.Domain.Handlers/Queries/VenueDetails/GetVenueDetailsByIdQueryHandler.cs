using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Shared.Settings;
using Loyalty.Data.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.VenueDetails;
using Loyalty.Domain.Handlers.Extensions;
using Loyalty.Domain.Handlers.Queries.Queries.VenueDetails;
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

        public async Task<GetVenueFullByIdQueryResult> Handle(GetVenueDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await
                (from venue in Context.Venues
                join details in Context.VenueDetails on venue.Id equals details.VenueId into venueDetails
                from details in venueDetails.DefaultIfEmpty()
                select new GetVenueFullByIdQueryResult
                {
                    Details = details.ToResult(),
                    Venue = venue.ToResult(),
                }).SingleOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
