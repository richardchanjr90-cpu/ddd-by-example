using System.Collections.Generic;
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
    public class GetLoyaltyProgramsQueryHandler
        : BaseDapperHandler, IRequestHandler<GetLoyaltyProgramsQuery, GetLoyaltyProgramsQueryResult>
    {
        private const string SelectQuery = @"SELECT [Id]
                                                  ,[Name]
                                                  ,[Description]
                                                  ,[StartDate]
                                                  ,[EndDate]
                                                  ,[VenueId]
                                                  ,[IsPublished]
                                                  ,[ExternalProgramUri] as ExternalProgramUri
                                              FROM [loyalty].[LoyaltyProgram]
                                          WHERE VenueId = @id";

        public GetLoyaltyProgramsQueryHandler(SqlConnection connection, IHttpContextAccessor accessor)
            : base(connection, accessor)
        {
        }

        public Task<GetLoyaltyProgramsQueryResult> Handle(
            GetLoyaltyProgramsQuery request,
            CancellationToken cancellationToken)
        {
            using (Connection)
            {
                Connection.Open();

                var rows = Connection.Query<GetLoyaltyProgramByIdQueryResult>(
                        SelectQuery,
                        new
                        {
                            id = request.VenueId
                        })
                    .ToList();

                return Task.FromResult(new GetLoyaltyProgramsQueryResult()
                {
                    Result = rows ?? new List<GetLoyaltyProgramByIdQueryResult>()
                });
            }
        }
    }
}