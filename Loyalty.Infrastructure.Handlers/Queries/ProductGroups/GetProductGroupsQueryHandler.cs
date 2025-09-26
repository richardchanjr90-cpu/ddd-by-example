using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.ProductGroups;
using Loyalty.Domain.Handlers.Queries.Queries.ProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.Product;
using Loyalty.Domain.Handlers.Queries.QueryResults.ProductGroup;
using Loyalty.Infrastructure.Handlers.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Queries.ProductGroups
{
    public class GetProductGroupsQueryHandler : BaseHandler, IGetProductGroupsQueryHandler
    {
        public GetProductGroupsQueryHandler(ILoyaltyDbContext context) 
            : base(context)
        {
        }

        public async Task<GetProductGroupsQueryResult> Handle(GetProductGroupsQuery request, CancellationToken cancellationToken)
        {
            var items = await (from lp in Context.ProductGroups
                where lp.VenueId == request.VenueId
                select lp).ToListAsync(cancellationToken);

            return new GetProductGroupsQueryResult
            {
                Result = items.ToResults()
            };
        }
    }
}