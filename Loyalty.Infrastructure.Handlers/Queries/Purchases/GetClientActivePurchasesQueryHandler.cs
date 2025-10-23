using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.Purchases;
using Loyalty.Domain.Handlers.Queries.Queries.Purchase;
using Loyalty.Domain.Handlers.Queries.QueryResults.Purchase;
using Loyalty.Infrastructure.Handlers.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Queries.Purchase
{
    public class GetClientActivePurchasesQueryHandler : BaseHandler, IGetClientActivePurchasesQueryHandler
    {
        public GetClientActivePurchasesQueryHandler(ILoyaltyDbContext context)
            : base(context)
        {
        }

        public async Task<GetActivePurchasesResult> Handle(GetClientActivePurchasesQuery request, CancellationToken cancellationToken)
        {
            //todo: add venue filtration
            var result = await (
                from lp in Context.LoyaltyPrograms.Where(lp => lp.VenueId == request.VenueId && lp.IsPublished)
                from lg in Context.LoyaltyProductGroups.Include(x => x.Rules)
                where lg.LoyaltyProgramId == lp.Id
            join purchase in Context.Purchases on lg.Id equals purchase.LoyaltyProductGroupId
                select new GetActivePurchaseResult
                {
                    LoyaltyProgramId = lg.LoyaltyProgramId,
                    LoyaltyGroupId = lg.Id,
                    // RuleType = lg.Rule.RuleType,
                    UserId = request.UserId,
                    Purchases = lg.Purchases.Where(x => x.LoyaltyProductGroupId == lg.Id 
                                                                                  && x.UserId == request.UserId
                                                                                  && !x.BurnDate.HasValue).ToResult()
                }).ToListAsync(cancellationToken);

            return new GetActivePurchasesResult
            {
                Result = result
            };
        }
    }
}
