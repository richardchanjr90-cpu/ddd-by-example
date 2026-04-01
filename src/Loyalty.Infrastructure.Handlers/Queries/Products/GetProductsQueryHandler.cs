using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Domain.Handlers.Queries.Queries.Product;
using Loyalty.Domain.Handlers.Queries.QueryResults.Product;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Infrastructure.Handlers.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Queries.Products
{
    public class GetProductsQueryHandler 
        : BaseHandler, IRequestHandler<GetProductsQuery, GetProductsQueryResult>
    {
        public GetProductsQueryHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<GetProductsQueryResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var items = await (from lp in Context.Products
                where lp.ProductGroupId == request.ProductGroupId
                select lp).ToListAsync(cancellationToken);

            return new GetProductsQueryResult
            {
                Result = items.ToResults()
            };
        }
    }
}