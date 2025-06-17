using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Shared.Settings;
using Loyalty.Data.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.Venues;
using Loyalty.Domain.Handlers.Queries.Queries.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using Microsoft.Extensions.Options;

namespace Loyalty.Domain.Handlers.Queries.Venues
{
    public class GetVenueByIdQueryHandler : BaseHandler, IGetVenueByIdQueryHandler
    {
        public GetVenueByIdQueryHandler(IMongoDataClient dbClient, IOptions<DbSettings> settings)
            : base(dbClient, settings)
        {
        }

        public Task<GetVenueByIdQueryResult> Handle(GetVenueByIdQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}