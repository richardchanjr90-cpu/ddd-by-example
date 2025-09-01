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
    public class GetVenueByIdQueryHandler : BaseHandler, IGetVenueByIdQueryHandler
    {
        public GetVenueByIdQueryHandler(ILoyaltyDbContext context, IOptions<DbSettings> settings)
            : base(context)
        {
        }

        public async Task<GetVenueByIdQueryResult> Handle(GetVenueByIdQuery request, CancellationToken cancellationToken)
        {
            var venue = await Context.Venues
                .Include(x => x.Location)
                .Where(x => x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            return venue?.ToResult();
        }
    }
}
