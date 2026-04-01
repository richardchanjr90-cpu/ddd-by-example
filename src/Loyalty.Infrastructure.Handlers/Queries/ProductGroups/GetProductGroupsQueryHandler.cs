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
    public class GetProductGroupsQueryHandler 
        : BaseHandler, IRequestHandler<GetProductGroupsQuery, GetProductGroupsQueryResult>
    {
        public GetProductGroupsQueryHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<GetProductGroupsQueryResult> Handle(GetProductGroupsQuery request,
            CancellationToken cancellationToken)
        {
            var items = await (from lp in Context.ProductGroups.Include(x => x.Products)
                where lp.VenueId == request.VenueId
                select lp).ToListAsync(cancellationToken);

            return new GetProductGroupsQueryResult
            {
                Result = items?.ToResults()
            };
        }
    }
}