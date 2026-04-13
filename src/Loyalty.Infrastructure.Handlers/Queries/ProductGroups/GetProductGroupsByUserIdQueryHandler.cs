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
    public class GetProductGroupsByUserIdQueryHandler
        : BaseDapperHandler, IRequestHandler<GetProductGroupsByUserIdQuery, GetProductGroupsByUserIdQueryResult>
    {
        private readonly SqlConnection connection;

        public GetProductGroupsByUserIdQueryHandler(SqlConnection connection, IHttpContextAccessor accessor)
            : base(connection, accessor)
        {
            this.connection = connection;
        }

        public async Task<GetProductGroupsByUserIdQueryResult> Handle(
            GetProductGroupsByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            var userId = Principal.GetUserId();

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
                                          FROM loyalty.ProductGroup pg 
                                    LEFT JOIN loyalty.Product p ON pg.Id = p.ProductGroupId
                                    JOIN loyalty.VenueWorker vw ON vw.VenueId = pg.VenueId
                                    JOIN loyalty.Worker w ON vw.WorkerId = w.Id
                                    WHERE w.WorkerId = @userId AND pg.Id = 1 AND pg.IsArchived = 0 AND p.IsArchived = 0 OR p.IsArchived IS NULL";

            await using (connection)
            {
                await connection.OpenAsync(cancellationToken);

                var dictionary = new Dictionary<long, GetProductGroupByIdQueryResult>();

                var rows = connection.Query<
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

                            if (product != null)
                            {
                                groupEntry.Products.Add(product);
                            }

                            return groupEntry;
                        }, new
                        {
                            userId
                        },
                        splitOn: "Id")
                    .Distinct()
                    .ToList();

                return new GetProductGroupsByUserIdQueryResult()
                {
                    Result = rows
                };
            }
        }
    }
}