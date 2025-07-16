using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Shared.Settings;
using Loyalty.Data.Contracts;
using Loyalty.Data.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Venues;
using Loyalty.Domain.Handlers.Extensions;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Loyalty.Domain.Handlers.Commands.Venues
{
    public class UpdateVenueCommandHandler : BaseHandler, IUpdateVenueCommandHandler
    {
        public UpdateVenueCommandHandler(IMongoDataClient dbClient, IOptions<DbSettings> settings)
            : base(dbClient, settings)
        {
        }

        public async Task<ICommandResult> Handle(UpdateVenueCommand request, CancellationToken cancellationToken)
        {
            var venue = request.ToSingle();
            var filter = Builders<Venue>.Filter.Eq(v => v.ItemId, venue.ItemId);

            var collection = Database.GetCollection<Venue>(nameof(Venue));
            await collection.ReplaceOneAsync(filter, venue, new UpdateOptions { IsUpsert = true }, cancellationToken);

            return new CommandResult
            {
                Success = true
            };
        }
    }
}
