using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.Products;
using Loyalty.Domain.Handlers.Queries.Queries.Product;
using Loyalty.Domain.Handlers.Queries.QueryResults.Product;
using Loyalty.Infrastructure.Handlers.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Queries.Products
{
    public class GetProductByIdQueryHandler : BaseHandler, IGetProductByIdQueryHandler
    {
        public GetProductByIdQueryHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<GetProductByIdQueryResult> Handle(GetProductByIdQuery request,
            CancellationToken cancellationToken)
        {
            var item = await (from lp in Context.Products
                where lp.Id == request.Id
                select lp).SingleOrDefaultAsync(cancellationToken);

            return item?.ToResult();
        }
    }
}