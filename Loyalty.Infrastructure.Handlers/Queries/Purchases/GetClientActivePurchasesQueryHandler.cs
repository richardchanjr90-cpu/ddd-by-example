using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Loyalty.Domain.Handlers.Contracts.Queries.Purchases;
using Loyalty.Domain.Handlers.Queries.Queries.Purchase;
using Loyalty.Domain.Handlers.Queries.QueryResults.Purchase;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.Handlers.Queries.Purchases
{
    public class GetClientActivePurchasesQueryHandler : BaseDapperHandler, IGetClientActivePurchasesQueryHandler
    {
        private readonly SqlConnection connection;

        public GetClientActivePurchasesQueryHandler(SqlConnection connection, IHttpContextAccessor accessor)
            : base(connection, accessor)
        {
            this.connection = connection;
        }

        public async Task<GetActivePurchasesResult> Handle(GetClientActivePurchasesQuery request,
            CancellationToken cancellationToken)
        {
            var getPrograms =
                @"SELECT 
                    lp.Id as ProgramId,
                    lp.[Name] as ProgramName,
                    lp.StartDate,
                    lp.EndDate,
                    lpg.Id as LgroupId,
                    lpg.[Name] as LgroupName,
                    pr.Id as ProductId,
                    pr.[Name] as ProductName,
                    lgr.[Rule] as [Rule],
                    lgr.RuleType as RuleType,
                    lgr.RuleVersion as RuleVersion,
                    total.total as Total
                    FROM loyalty.LoyaltyProgram lp
                    JOIN loyalty.LoyaltyProductGroup lpg ON lpg.LoyaltyProgramId = lp.Id
                    JOIN loyalty.LoyaltyGroupRule lgr ON lgr.LoyaltyProductGroupId = lpg.Id
                    JOIN loyalty.ProductGroup pg ON pg.Id = lpg.ProductGroupId
					JOIN loyalty.Purchase pur ON pur.LoyaltyProductGroupId = lpg.Id
                    LEFT JOIN loyalty.Product pr ON pr.Id = pur.ProductId
					LEFT JOIN (SELECT LoyaltyProductGroupId, 
					COALESCE(SUM([Value]), 0) as total 
					FROM loyalty.Purchase
					WHERE BurnDate IS NULL AND UserId = @UserId
					GROUP BY LoyaltyProductGroupId) as total ON total.LoyaltyProductGroupId = lpg.Id			
                    WHERE pur.UserId = @UserId 
					AND lp.VenueId = @VenueId
					AND lp.IsArchived = 0 ";

            var programs = connection.Query(getPrograms, new
            {
                request.VenueId, request.UserId
            }).ToList();

            var programsDistinct = programs
                .GroupBy(p => p.ProgramId)
                .Select(g => g.First())
                .Select(x => new GetActivePurchaseResult
                {
                    LoyaltyProgramId = x.ProgramId,
                    Name = x.ProgramName,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Groups = programs
                        .Where(y => y.ProgramId == x.ProgramId)
                        .GroupBy(p => p.LgroupId)
                        .Select(g => g.First())
                        .Select(z => new GroupPurchaseResult
                        {
                            RuleVersion = z.RuleVersion,
                            Rule = z.Rule,
                            Total = z.Total,
                            RuleType = z.RuleType,
                            GroupName = z.LgroupName,
                            LoyaltyProductGroupId = z.LgroupId,
                            Products = programs
                                .Where(q => q.LgroupId == z.LgroupId && q.ProductId > 0)
                                .DefaultIfEmpty(null)
                                .Select(d => new ProductPurchaseResult
                                {
                                    Name = d?.ProductName,
                                    Id = d?.ProductId
                                }).ToList()
                        }).ToList()
                })
                .ToList();

            return new GetActivePurchasesResult
            {
                Result = programsDistinct
            };
        }
    }
}