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
    public class GetProductByIdQueryHandler
        : BaseDapperHandler, IRequestHandler<GetProductByIdQuery, GetProductByIdQueryResult>
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
                                          WHERE Id = @id AND IsArchived = 0";

        public GetProductByIdQueryHandler(SqlConnection connection, IHttpContextAccessor accessor)
            : base(connection, accessor)
        {
        }

        public Task<GetProductByIdQueryResult> Handle(
            GetProductByIdQuery request,
            CancellationToken cancellationToken)
        {
            using (Connection)
            {
                Connection.Open();

                var row = Connection.QuerySingleOrDefault<GetProductByIdQueryResult>(
                    SelectQuery,
                    new
                    {
                        id = request.Id
                    });

                return Task.FromResult(row);
            }
        }
    }
}