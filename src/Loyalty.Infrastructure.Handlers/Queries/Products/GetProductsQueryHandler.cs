using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Loyalty.Domain.Handlers.Queries.Queries.Product;
using Loyalty.Domain.Handlers.Queries.QueryResults.Product;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace Loyalty.Infrastructure.Handlers.Queries.Products
{
    public class GetProductsQueryHandler
        : BaseDapperHandler, IRequestHandler<GetProductsQuery, GetProductsQueryResult>
    {
        private const string SelectQuery = @"SELECT [Id]
                                              ,[Name]
                                              ,[Icon]
                                              ,[ProductGroupId]
                                              ,[ImageUri]
                                              ,[Price]
                                              ,[IsAvailableForOrder]
                                              ,[ExternalUid]
                                              ,[Description]
                                          FROM [loyalty].[Product]
                                          WHERE ProductGroupId = @id AND IsArchived = 0";

        public GetProductsQueryHandler(SqlConnection connection, IHttpContextAccessor accessor)
            : base(connection, accessor)
        {
        }

        public Task<GetProductsQueryResult> Handle(
            GetProductsQuery request,
            CancellationToken cancellationToken)
        {
            using (Connection)
            {
                Connection.Open();

                var rows = Connection.Query<GetProductByIdQueryResult>(
                    SelectQuery,
                    new
                    {
                        id = request.ProductGroupId
                    })
                    .ToList();

                return Task.FromResult(new GetProductsQueryResult()
                {
                    Result = rows ?? new List<GetProductByIdQueryResult>()
                });
            }
        }
    }
}