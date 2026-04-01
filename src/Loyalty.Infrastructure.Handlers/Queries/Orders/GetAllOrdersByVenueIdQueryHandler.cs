using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Domain.Handlers.Queries.Queries.Orders;
using Loyalty.Domain.Handlers.Queries.QueryResults.Orders;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Infrastructure.Handlers.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Queries.Orders
{
    public class GetAllOrdersByVenueIdQueryHandler
        : BaseHandler, IRequestHandler<GetAllOrdersByVenueIdQuery, GetOrdersByVenueIdQueryResult>
    {
        public GetAllOrdersByVenueIdQueryHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<GetOrdersByVenueIdQueryResult> Handle(
            GetAllOrdersByVenueIdQuery request,
            CancellationToken cancellationToken)
        {
            var orders = await Context.Orders
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
                .Where(x => x.VenueId == request.VenueId)
                .ToListAsync(cancellationToken);

            return new GetOrdersByVenueIdQueryResult
            {
                Orders = orders.ToResults()
            };
        }
    }
}