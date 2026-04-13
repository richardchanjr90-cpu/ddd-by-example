using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Domain.Handlers.Queries.Queries.LoyaltyProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.Rules;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Loyalty.Infrastructure.Handlers.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Queries.LoyaltyProductGroups
{
    public class GetLoyaltyProductGroupsQueryHandler 
        : BaseHandler, IRequestHandler<GetLoyaltyProductGroupQuery, GetLoyaltyProductGroupsQueryResult>
    {
        public GetLoyaltyProductGroupsQueryHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<GetLoyaltyProductGroupsQueryResult> Handle(GetLoyaltyProductGroupQuery request,
            CancellationToken cancellationToken)
        {
            var items = Context.LoyaltyProductGroups
                .Include(x => x.Rules)
                .Include(x => x.Group)
                //.ThenInclude(x => x.Products)
                .Where(x => x.LoyaltyProgramId == request.LoyaltyProgramId)
                .ToList()
                .Select(lp => new GetLoyaltyProductGroupByIdQueryResult
                {
                    Id = lp.Id,
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