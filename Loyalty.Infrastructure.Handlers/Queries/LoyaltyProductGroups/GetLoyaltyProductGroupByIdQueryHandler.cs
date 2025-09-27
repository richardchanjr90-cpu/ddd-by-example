using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.LoyaltyProductGroups;
using Loyalty.Domain.Handlers.Queries.Queries.LoyaltyProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProductGroup;
using Loyalty.Infrastructure.Handlers.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Queries.LoyaltyProductGroups
{
    public class GetLoyaltyProductGroupByIdQueryHandler : BaseHandler, IGetLoyaltyProductGroupByIdQueryHandler
    {
        public GetLoyaltyProductGroupByIdQueryHandler(ILoyaltyDbContext context) 
            : base(context)
        {
        }

        public async Task<GetLoyaltyProductGroupByIdQueryResult> Handle(GetLoyaltyProductGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await (from lp in Context.LoyaltyProductGroups
                    .Include(x => x.Rules)
                    .Include(x => x.ProductGroup)
                    .ThenInclude(x => x.Products)
                              where lp.Id == request.Id
                              select new GetLoyaltyProductGroupByIdQueryResult
                              {
                                  Id = lp.Id,
                                  IsArchived = lp.IsArchived,
                                  LoyaltyProgramId = lp.LoyaltyProgramId,
                                  Description = lp.Description,
                                  Name = lp.Name,
                                  //RuleType = lp.Rule.RuleType,
                                  ProductGroup = lp.ProductGroup.ToResult()
                              }).SingleAsync(cancellationToken);

            return item;
        }
    }
}
