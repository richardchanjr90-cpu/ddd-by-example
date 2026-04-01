using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Loyalty.Core.Entities;
using Loyalty.Domain.Handlers.Queries.Queries.Code;
using Loyalty.Domain.Handlers.Queries.QueryResults.Code;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace Loyalty.Infrastructure.Handlers.Queries.Code
{
    public class GetUserInfoByCodeQueryHandler 
        : BaseDapperHandler, IRequestHandler<GetUserInfoByCodeQuery, GetUserInfoByCodeQueryResult>
    {
        private readonly SqlConnection connection;

        public GetUserInfoByCodeQueryHandler(SqlConnection connection, IHttpContextAccessor accessor)
            : base(connection, accessor)
        {
            this.connection = connection;
        }

        public async Task<GetUserInfoByCodeQueryResult> Handle(GetUserInfoByCodeQuery request,
            CancellationToken cancellationToken)
        {
            var getUserByCodeQuery = @"SELECT TOP 1 [UserId]
                                      ,[CodeValue]
                                      ,[ExpirationDate]
                                  FROM [loyalty].[UserCode]
                                  WHERE CodeValue = @Code";

            var result = connection.QueryFirstOrDefault<UserCode>(getUserByCodeQuery, new
            {
                request.Code
            });

            return new GetUserInfoByCodeQueryResult
            {
                UserId = result?.UserId
            };
        }
    }
}
