using System;
using System.Collections.Generic;
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
using SecurityDriven.TinyORM.Extensions;
using DbContext = SecurityDriven.TinyORM.DbContext;

namespace Loyalty.Infrastructure.Handlers.Queries.Venues
{
    public class GetVenuesQueryHandler: IGetVenuesQueryHandler
    {
        private readonly DbContext context;

        public GetVenuesQueryHandler(DbContext context, IOptions<DbSettings> settings)
            //: base(context)
        {
            this.context = context;
        }

        public async Task<GetVenuesQueryResult> Handle(GetVenuesQuery request, CancellationToken cancellationToken)
        {
            //var venues = await Context.Venues
            //    .Include(x => x.Location)
            //    .Include(x => x.Workers)
            //    .Where(x => x.Workers.Select(y => y.WorkerId).Contains(request.UserId))
            //    .ToListAsync(cancellationToken);
            var query = "SELECT v.Id FROM loyalty.Venue v " +
                        "JOIN loyalty.Worker w ON v.Id = w.VenueId " +
                        "JOIN loyalty.Location l ON l.VenueId = v.Id " +
                        "WHERE v.IsArchived = 0 AND w.WorkerId = @workerId";

            var parameters = new Dictionary<string, (object, Type)>();
            parameters.Add("@workerId", request.UserId.WithType());
            var rows = (await context.QueryAsync(query, parameters), cancellationToken);

            return new GetVenuesQueryResult
            {
                Venues = null 
            };
        }
    }
}