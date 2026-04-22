using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Loyalty.Domain.Handlers.Queries.Queries.LoyaltyProgram;
using Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProgram;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace Loyalty.Infrastructure.Handlers.Queries.LoyaltyPrograms
{
    public class GetLoyaltyProgramByIdQueryHandler
        : BaseDapperHandler,  IRequestHandler<GetLoyaltyProgramByIdQuery, GetLoyaltyProgramByIdQueryResult>
    {
        private const string SelectQuery = @"SELECT [Id]
                                                  ,[Name]
                                                  ,[Description]
                                                  ,[StartDate]
                                                  ,[EndDate]
                                                  ,[VenueId]
                                                  ,[IsPublished]
                                                  ,[Url]
                                              FROM [loyalty].[LoyaltyProgram]
                                          WHERE o.Id = @id";

        public GetLoyaltyProgramByIdQueryHandler(SqlConnection connection, IHttpContextAccessor accessor)
            : base(connection, accessor)
        {
        }

        public Task<GetLoyaltyProgramByIdQueryResult> Handle(
            GetLoyaltyProgramByIdQuery request,
            CancellationToken cancellationToken)
        {
            using (Connection)
            {
                Connection.Open();

                var row = Connection.QuerySingle<GetLoyaltyProgramByIdQueryResult>(
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