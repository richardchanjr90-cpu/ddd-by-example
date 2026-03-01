using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.LoyaltyProductGroups;
using Loyalty.Domain.Handlers.Queries.Queries.LoyaltyProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.Rules;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Infrastructure.Handlers.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Queries.LoyaltyProductGroups
{
    public class GetLoyaltyProductGroupByIdQueryHandler : BaseHandler, IGetLoyaltyProductGroupByIdQueryHandler
    {
        public GetLoyaltyProductGroupByIdQueryHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<GetLoyaltyProductGroupByIdQueryResult> Handle(GetLoyaltyProductGroupByIdQuery request,
            CancellationToken cancellationToken)
        {
            var item = Context.LoyaltyProductGroups.Include(x => x.Group)
                .ThenInclude(x => x.Products)
                .Include(x => x.Rules)
                .Where(x => x.Id == request.Id && x.LoyaltyProgramId == request.ProgramId)
                .ToList()
                .Select(lp => new GetLoyaltyProductGroupByIdQueryResult
                {
                    Id = lp.Id,
                    LoyaltyProgramId = lp.LoyaltyProgramId,
                    Description = lp.Description,
                    Name = lp.Name,
                    Rules = new GetRuleByIdQueryResult { Rules = lp.Rules.ToList().ToResults() },
                    ProductGroup = lp.Group.ToResult()
                }).SingleOrDefault();

            return item;
        }
    }
}