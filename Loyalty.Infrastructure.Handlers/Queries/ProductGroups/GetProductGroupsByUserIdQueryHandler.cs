using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Enums;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.ProductGroups;
using Loyalty.Domain.Handlers.Queries.Queries.ProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.Product;
using Loyalty.Domain.Handlers.Queries.QueryResults.ProductGroup;
using Loyalty.Infrastructure.Handlers.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Queries.ProductGroups
{
    public class GetProductGroupsByUserIdQueryHandler : BaseHandler, IGetProductGroupsByUserIdQueryHandler
    {
        public GetProductGroupsByUserIdQueryHandler(ILoyaltyDbContext context) 
            : base(context)
        {
        }

        public async Task<GetProductGroupsByUserIdQueryResult> Handle(GetProductGroupsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var items = await (from worker in Context.Workers
                join prGroup in Context.ProductGroups.Include(x => x.Products) on worker.VenueId equals prGroup.VenueId
                               where worker.Role >= VenueUserRole.Manager && worker.WorkerId == request.UserId
                               select prGroup).ToListAsync(cancellationToken);

            return new GetProductGroupsByUserIdQueryResult
            {
                Result = items?.ToResults()
            };
        }
    }
}