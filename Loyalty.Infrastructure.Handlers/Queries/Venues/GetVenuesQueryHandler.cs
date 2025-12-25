using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Common.Shared.Settings;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Handlers.Contracts.Queries.Venues;
using Loyalty.Domain.Handlers.Queries.Queries.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using Loyalty.Infrastructure.Handlers.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Queries.Venues
{
    public class GetVenuesQueryHandler : BaseDapperHandler, IGetVenuesQueryHandler
    {
        private readonly SqlConnection connection;

        public GetVenuesQueryHandler(SqlConnection connection, IHttpContextAccessor accessor)
            : base(connection, accessor)
        {
            this.connection = connection;
        }

        public Task<GetVenuesQueryResult> Handle(GetVenuesQuery request, CancellationToken cancellationToken)
        {
            var getItems = @"SELECT * FROM loyalty.Venue WHERE Id in @ids";
            var ids = Principal.GetVenueIds();
            var venues = connection.Query<Venue>(getItems, new
            {
                ids
            }).ToList();

            var result = new GetVenuesQueryResult
            {
                Venues = venues.ToResults()
            };

            return Task.FromResult(result);
        }
    }
}