using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.Products;
using Loyalty.Domain.Handlers.Queries.Queries.Product;
using Loyalty.Domain.Handlers.Queries.QueryResults.Product;
using Loyalty.Infrastructure.Handlers.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Queries.Products
{
    public class GetProductsQueryHandler : BaseHandler, IGetProductsQueryHandler
    {
        public GetProductsQueryHandler(ILoyaltyDbContext context) 
            : base(context)
        {
        }

        public async Task<GetProductsQueryResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var items = await (from lp in Context.Products
                where lp.VenueId == request.VenueId
                               select lp).ToListAsync(cancellationToken);

            return new GetProductsQueryResult
            {
                Result = items.ToResults()
            };
        }
    }
}