using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.ProductGroups;
using Loyalty.Domain.Handlers.Queries.Queries.ProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProgram;
using Loyalty.Domain.Handlers.Queries.QueryResults.ProductGroup;
using Loyalty.Infrastructure.Handlers.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Queries.ProductGroups
{
    public class GetProductGroupByIdQueryHandler : BaseHandler, IGetProductGroupByIdQueryHandler
    {
        public GetProductGroupByIdQueryHandler(ILoyaltyDbContext context) 
            : base(context)
        {
        }

        public async Task<GetProductGroupByIdQueryResult> Handle(GetProductGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await (from lp in Context.ProductGroups
                    .Include(x => x.Products)
                where lp.Id == request.Id
                select lp).SingleOrDefaultAsync(cancellationToken);

            return item.ToResult();
        }
    }
}
