using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Handlers.Contracts.Queries.ProductGroups;
using Loyalty.Domain.Handlers.Queries.Queries.ProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.ProductGroup;
using Loyalty.Infrastructure.Handlers.Extensions;
using Loyalty.Shared.Contracts.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Queries.ProductGroups
{
    public class GetProductGroupsByUserIdQueryHandler : BaseDapperHandler, IGetProductGroupsByUserIdQueryHandler
    {
        private readonly SqlConnection connection;

        public GetProductGroupsByUserIdQueryHandler(SqlConnection connection, IHttpContextAccessor accessor)
            : base(connection, accessor)
        {
            this.connection = connection;
        }

        public async Task<GetProductGroupsByUserIdQueryResult> Handle(GetProductGroupsByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            var userId = request.UserId;

            var getItems =
                @"SELECT pg.* FROM 
                    loyalty.ProductGroup pg 
                    JOIN loyalty.VenueWorker vw ON vw.VenueId = pg.VenueId
                    JOIN loyalty.Worker w ON vw.WorkerId = w.Id
                    WHERE w.WorkerId = @userId AND w.IsArchived = 0 AND pg.IsArchived = 0";

            var items = connection.Query<ProductGroup>(getItems, new
            {
                userId
            }).ToList();

            return new GetProductGroupsByUserIdQueryResult
            {
                Result = items.ToResults()
            };
        }
    }
}