using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Handlers.Contracts.Queries.ProductGroups;
using Loyalty.Domain.Handlers.Queries.Queries.ProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.ProductGroup;
using Loyalty.Infrastructure.Handlers.Extensions;
using Loyalty.Shared.Contracts.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
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
            var groups = new List<ProductGroup>();
            var userId = request.UserId;

            var getItems =
                @"SELECT pg.* FROM 
                    loyalty.ProductGroup pg 
                    JOIN loyalty.VenueWorker vw ON vw.VenueId = pg.VenueId
                    JOIN loyalty.Worker w ON vw.WorkerId = w.Id
                    WHERE w.WorkerId = @userId AND w.IsArchived = 0 AND pg.IsArchived = 0";

            var getProducts = @"SELECT * FROM loyalty.Product WHERE ProductGroupId in @productIds AND IsArchived = 0";
            using (var transaction = new TransactionScope())
            {
                groups = connection.Query<ProductGroup>(getItems, new
                {
                    userId
                }).ToList();

                var productIds = groups.Select(x => x.Id);

                var products = connection.Query<Product>(getProducts, new
                {
                    productIds
                }).ToList();

                transaction.Complete();

                if (groups.Count > 0 && products.Count > 0)
                {
                    foreach (var group in groups)
                    {
                        group.Products = new List<Product>();
                        var selected = products.Where(x => x.ProductGroupId == group.Id);

                        foreach (var product in selected)
                        {
                            group.Products.Add(product);
                        }
                    }
                }
            }

            return new GetProductGroupsByUserIdQueryResult
            {
                Result = groups.ToResults()
            };
        }
    }
}