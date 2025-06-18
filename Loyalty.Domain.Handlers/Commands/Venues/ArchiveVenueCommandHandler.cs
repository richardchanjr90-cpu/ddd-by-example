using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Shared.Settings;
using Loyalty.Data.Contracts;
using Loyalty.Data.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Venues;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Loyalty.Domain.Handlers.Commands.Venues
{
    public class ArchiveVenueCommandHandler : BaseHandler, IArchiveVenueCommandHandler
    {
        public ArchiveVenueCommandHandler(IMongoDataClient dbClient, IOptions<DbSettings> settings)
            : base(dbClient, settings)
        {
        }

        public async Task<ICommandResult> Handle(ArchiveVenueCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
