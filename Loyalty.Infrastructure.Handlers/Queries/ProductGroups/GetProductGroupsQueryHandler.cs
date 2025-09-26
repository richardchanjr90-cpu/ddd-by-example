using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.ProductGroups;
using Loyalty.Domain.Handlers.Queries.Queries.ProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.ProductGroup;

namespace Loyalty.Infrastructure.Handlers.Queries.ProductGroups
{
    public class GetProductGroupsQueryHandler : BaseHandler, IGetProductGroupsQueryHandler
    {
        public GetProductGroupsQueryHandler(ILoyaltyDbContext context) : base(context)
        {
        }

        public async Task<GetProductGroupsQueryResult> Handle(GetProductGroupsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}