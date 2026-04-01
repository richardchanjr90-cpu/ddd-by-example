using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Domain.Handlers.Queries.Queries.ProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.ProductGroup;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Infrastructure.Handlers.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Queries.ProductGroups
{
    public class GetProductGroupByIdQueryHandler 
        : BaseHandler, IRequestHandler<GetProductGroupByIdQuery, GetProductGroupByIdQueryResult>
    {
        public GetProductGroupByIdQueryHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<GetProductGroupByIdQueryResult> Handle(
            GetProductGroupByIdQuery request,
            CancellationToken cancellationToken)
        {
            var item = await (from lp in Context.ProductGroups
                    .Include(x => x.Products)
                where lp.Id == request.Id
                select lp).SingleOrDefaultAsync(cancellationToken);

            return item?.ToResult();
        }
    }
}