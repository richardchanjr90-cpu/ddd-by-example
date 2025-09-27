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
    public class GetLoyaltyProductGroupsQueryHandler : BaseHandler, IGetLoyaltyProductGroupsQueryHandler
    {
        public GetLoyaltyProductGroupsQueryHandler(ILoyaltyDbContext context) 
            : base(context)
        {
        }

        public async Task<GetLoyaltyProductGroupsQueryResult> Handle(GetLoyaltyProductGroupQuery request, CancellationToken cancellationToken)
        {
            var items = await (from lp in Context.LoyaltyProductGroups
                    .Include(x => x.Rules)
                    .Include(x => x.ProductGroup)
                    .ThenInclude(x => x.Products)
                               where lp.Id == request.LoyaltyProgramId
                               select new GetLoyaltyProductGroupByIdQueryResult
                               {
                                   Id = lp.Id,
                                   IsArchived = lp.IsArchived,
                                   LoyaltyProgramId = lp.LoyaltyProgramId,
                                   Description = lp.Description,
                                   Name = lp.Name,
                                //   RuleType = lp.Rule.RuleType,
                                   ProductGroup = lp.ProductGroup.ToResult()
                               }).ToListAsync(cancellationToken);

            return new GetLoyaltyProductGroupsQueryResult
            {
                Result = items
            };
        }
    }
}