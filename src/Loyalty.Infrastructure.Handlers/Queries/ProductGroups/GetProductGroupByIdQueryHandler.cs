using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Handlers.Queries.Queries.ProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.Product;
using Loyalty.Domain.Handlers.Queries.QueryResults.ProductGroup;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace Loyalty.Infrastructure.Handlers.Queries.ProductGroups
{
    public class GetProductGroupByIdQueryHandler
        : BaseDapperHandler, IRequestHandler<GetProductGroupByIdQuery, GetProductGroupByIdQueryResult>
    {
        private readonly SqlConnection connection;

        public GetProductGroupByIdQueryHandler(SqlConnection connection, IHttpContextAccessor accessor)
            : base(connection, accessor)
        {
            this.connection = connection;
        }

        public async Task<GetProductGroupByIdQueryResult> Handle(
            GetProductGroupByIdQuery request,
            CancellationToken cancellationToken)
        {
            var userId = Principal.GetUserId();
            var id = request.Id;

            const string selectQuery = @"SELECT pg.[Id]
                                          ,pg.[VenueId]
                                          ,pg.[Name]
                                          ,pg.[Icon]
                                          ,p.[Id]
                                          ,p.[Name]
                                          ,p.[Icon]
                                          ,p.[ProductGroupId]
                                          ,p.[Price]
                                          ,p.[IsAvailableForOrder]
                                          ,p.[ExternalUid]
                                          ,p.[Description]
                                          ,p.[ImageUri]
                                          ,p.[IsArchived]
                                          FROM loyalty.ProductGroup pg 
                                    LEFT JOIN loyalty.Product p ON pg.Id = p.ProductGroupId
                                    JOIN loyalty.VenueWorker vw ON vw.VenueId = pg.VenueId
                                    JOIN loyalty.Worker w ON vw.WorkerId = w.Id
                                    WHERE w.WorkerId = @userId AND pg.Id = 1 AND pg.IsArchived = 0";
            await using (connection)
            {
                await connection.OpenAsync(cancellationToken);

                var dictionary = new Dictionary<long, GetProductGroupByIdQueryResult>();

                var row = connection.Query<
                        GetProductGroupByIdQueryResult,
                        GetProductByIdQueryResult,
                        GetProductGroupByIdQueryResult>(
                        selectQuery,
                        (group, product) =>
                        {
                            if (!dictionary.TryGetValue(group.Id, out var groupEntry))
                            {
                                groupEntry = group;
                                groupEntry.Products = new List<GetProductByIdQueryResult>();

                                group.IsAvailableForOrder = true;
                                dictionary.Add(group.Id, group);
                            }

                            groupEntry.IsAvailableForOrder =
                                groupEntry.IsAvailableForOrder && (product?.IsAvailableForOrder ?? false);

                            if (product != null && !product.IsArchived)
                            {
                                groupEntry.Products.Add(product);
                            }

                            return groupEntry;
                        }, new
                        {
                            id,
                            userId
                        },
                        splitOn: "Id")
                    .Distinct()
                    .SingleOrDefault();

                return row;
            }
        }
    }
}