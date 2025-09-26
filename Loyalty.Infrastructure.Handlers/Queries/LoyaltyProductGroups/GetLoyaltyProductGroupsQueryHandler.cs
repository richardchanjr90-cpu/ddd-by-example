using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.LoyaltyProductGroups;
using Loyalty.Domain.Handlers.Queries.Queries.LoyaltyProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProductGroup;

namespace Loyalty.Infrastructure.Handlers.Queries.LoyaltyProductGroups
{
    public class GetLoyaltyProductGroupsQueryHandler : BaseHandler, IGetLoyaltyProductGroupsQueryHandler
    {
        public GetLoyaltyProductGroupsQueryHandler(ILoyaltyDbContext context) : base(context)
        {
        }

        public Task<GetLoyaltyProductGroupsQueryResult> Handle(GetLoyaltyProductGroupQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}