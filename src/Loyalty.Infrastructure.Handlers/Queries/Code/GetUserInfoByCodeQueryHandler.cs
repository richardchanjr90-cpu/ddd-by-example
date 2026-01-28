using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Loyalty.Core.Contracts.Connection;
using Loyalty.Core.Entities;
using Loyalty.Core.UserEntities;
using Loyalty.Domain.Handlers.Contracts.Queries.Code;
using Loyalty.Domain.Handlers.Queries.Queries.Code;
using Loyalty.Domain.Handlers.Queries.QueryResults.Code;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.Handlers.Queries.LoyaltyProductGroups
{
    public class GetUserInfoByCodeQueryHandler : BaseDapperHandler, IGetUserInfoByCodeQueryHandler
    {
        private readonly IUserDbConnection connection;

        public GetUserInfoByCodeQueryHandler(IUserDbConnection connection, IHttpContextAccessor accessor)
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

            var result = connection.QuerySingle<UserCode>(getUserByCodeQuery, new
            {
                request.Code
            });

            return new GetUserInfoByCodeQueryResult
            {
                UserId = result.UserId
            };
        }
    }
}
