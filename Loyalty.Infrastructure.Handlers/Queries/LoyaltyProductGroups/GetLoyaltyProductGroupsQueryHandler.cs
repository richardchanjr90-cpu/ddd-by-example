using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.LoyaltyProductGroups;
using Loyalty.Domain.Handlers.Queries.Queries.LoyaltyProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.Rules;
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
            var items = Context.LoyaltyProductGroups
                    .Include(x => x.Rules)
                    .Include(x => x.Group)
                    .ThenInclude(x => x.Products)
                    .Where(x => x.Id == request.LoyaltyProgramId)
                    .ToList()
                    .Select(lp => new GetLoyaltyProductGroupByIdQueryResult
                    {
                        Id = lp.Id,
                        IsArchived = lp.IsArchived,
                        LoyaltyProgramId = lp.LoyaltyProgramId,
                        Description = lp.Description,
                        Name = lp.Name,
                        Rules = new GetRuleByIdQueryResult
                        {
                            Rules = lp.Rules.ToList().ToResults()
                        },
                        ProductGroup = lp.Group.ToResult()
                    }).ToList();

            return new GetLoyaltyProductGroupsQueryResult
            {
                Result = items
            };
        }
    }
}