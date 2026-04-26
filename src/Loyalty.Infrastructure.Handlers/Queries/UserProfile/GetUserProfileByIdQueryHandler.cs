using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Loyalty.Domain.Handlers.Queries.Queries.UserProfile;
using Loyalty.Domain.Handlers.Queries.QueryResults.UserProfile;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace Loyalty.Infrastructure.Handlers.Queries.UserProfile
{
    public class GetUserProfileByIdQueryHandler
        : BaseDapperHandler, IRequestHandler<GetUserProfileByIdQuery, GetUserProfileByIdQueryResult>
    {
        private const string SelectQuery = @"SELECT
                                               [Phone]
                                              ,[Name]
                                              ,[LastName]
                                              ,[PhotoUri]
                                              ,[City]
                                          FROM [loyalty].[Worker]
                                          WHERE WorkerId = @id";

        public GetUserProfileByIdQueryHandler(SqlConnection connection, IHttpContextAccessor accessor)
            : base(connection, accessor)
        {
        }

        public Task<GetUserProfileByIdQueryResult> Handle(
            GetUserProfileByIdQuery request,
            CancellationToken cancellationToken)
        {
            using (Connection)
            {
                Connection.Open();

                var row = Connection.QuerySingle<GetUserProfileByIdQueryResult>(
                    SelectQuery,
                    new
                    {
                        id = request.UserId
                    });

                return Task.FromResult(row);
            }
        }
    }
}