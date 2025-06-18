using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Shared.Settings;
using Loyalty.Data.Contracts;
using Loyalty.Data.Entities;
using Loyalty.Domain.Handlers.Contracts.Queries.Venues;
using Loyalty.Domain.Handlers.Extensions;
using Loyalty.Domain.Handlers.Queries.Queries.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Loyalty.Domain.Handlers.Queries.Venues
{
    public class GetVenuesByUserIdQueryHandler : BaseHandler, IGetVenuesByUserIdQueryHandler
    {
        public GetVenuesByUserIdQueryHandler(IMongoDataClient dbClient, IOptions<DbSettings> settings)
            : base(dbClient, settings)
        {
        }

        public async Task<GetVenuesByUserIdQueryResult> Handle(GetVenuesByUserIdQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<Venue>.Filter.Eq(x => request.UserId, request.UserId);
            var collection = Database.GetCollection<Venue>(nameof(Venue));
            var itemsList = await collection.Find(filter).ToListAsync(cancellationToken);
            return new GetVenuesByUserIdQueryResult
            {
                Venues = itemsList.ToResults()
            };
        }
    }
}