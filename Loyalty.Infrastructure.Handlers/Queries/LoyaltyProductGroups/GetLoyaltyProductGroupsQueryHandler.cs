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
                    .Include(x => x.Group)
                    .ThenInclude(x => x.Products)
                               where lp.Id == request.LoyaltyProgramId
                               select new GetLoyaltyProductGroupByIdQueryResult
                               {
                                   Id = lp.Id,
                                   IsArchived = lp.IsArchived,
                                   LoyaltyProgramId = lp.LoyaltyProgramId,
                                   Description = lp.Description,
                                   Name = lp.Name,
                                   //Rule = lp.Rules,
                                   ProductGroup = lp.Group.ToResult()
                               }).ToListAsync(cancellationToken);

            return new GetLoyaltyProductGroupsQueryResult
            {
                Result = items
            };
        }
    }
}