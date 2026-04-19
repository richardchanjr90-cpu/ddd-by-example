using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Domain.Handlers.Queries.Queries.LoyaltyProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.ProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.Rules;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace Loyalty.Infrastructure.Handlers.Queries.LoyaltyProductGroups
{
    public class GetLoyaltyProductGroupByIdQueryHandler
        : BaseDapperHandler, IRequestHandler<GetLoyaltyProductGroupByIdQuery, GetLoyaltyProductGroupByIdQueryResult>
    {
        private readonly SqlConnection connection;
        private readonly IHttpContextAccessor accessor;

        private const string SelectQuery = @"SELECT 
                                                   lpg.[Id]
                                                  ,lpg.[LoyaltyProgramId]
                                                  ,lpg.[Name]
                                                  ,lpg.[Description]
                                                  ,pg.[Id]
                                                  ,pg.[VenueId]
                                                  ,pg.[Name]
                                                  ,pg.[Icon]
                                                  ,lpr.[Id]
                                                  ,lpr.[Rule]
                                                  ,lpr.[RuleType]
                                                  ,lpr.[RuleVersion] 
                                              FROM [loyalty].[LoyaltyProductGroup] lpg 
                                              JOIN loyalty.LoyaltyGroupRule lpr ON lpg.Id = lpr.LoyaltyProductGroupId
                                              JOIN loyalty.ProductGroup pg ON lpg.ProductGroupId = pg.Id
                                              WHERE lpg.LoyaltyProgramId = @programId AND lpg.Id = @id";

        public GetLoyaltyProductGroupByIdQueryHandler(SqlConnection connection, IHttpContextAccessor accessor)
            : base(connection, accessor)
        {
            this.connection = connection;
            this.accessor = accessor;
        }

        public async Task<GetLoyaltyProductGroupByIdQueryResult> Handle(
            GetLoyaltyProductGroupByIdQuery request,
            CancellationToken cancellationToken)
        {
            var programId = request.ProgramId;
            var id = request.Id;

            await using (connection)
            {
                await connection.OpenAsync(cancellationToken);

                var loyaltyGroupDictionary = new Dictionary<long, GetLoyaltyProductGroupByIdQueryResult>();

                var rows = Connection.Query<
                        GetLoyaltyProductGroupByIdQueryResult,
                        GetProductGroupByIdQueryResult,
                        GetSingleRuleByIdQueryResult,
                        GetLoyaltyProductGroupByIdQueryResult>(
                        SelectQuery,
                        (loyaltyGroup, group, rule) =>
                        {
                            if (!loyaltyGroupDictionary.TryGetValue(loyaltyGroup.Id, out var loyaltyGroupEntry))
                            {
                                loyaltyGroupEntry = loyaltyGroup;
                                loyaltyGroupEntry.Rules = new GetRuleByIdQueryResult();
                                loyaltyGroupEntry.ProductGroup = group;

                                loyaltyGroupDictionary.Add(loyaltyGroup.Id, loyaltyGroup);
                            }

                            loyaltyGroupEntry.Rules.Rules.Add(rule);

                            return loyaltyGroupEntry;
                        }, new
                        {
                            programId,
                            id
                        },
                        splitOn: "Id")
                    .Distinct()
                    .SingleOrDefault();

                return rows;
            }
        }
    }
}