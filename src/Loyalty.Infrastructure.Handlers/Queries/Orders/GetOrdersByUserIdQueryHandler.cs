using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Domain.Handlers.Queries.Queries.Orders;
using Loyalty.Domain.Handlers.Queries.QueryResults.Orders;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Loyalty.Infrastructure.Handlers.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Queries.Orders
{
    public class GetOrdersByUserIdQueryHandler      
        : BaseHandler, IRequestHandler<GetOrdersByUserIdQuery, GetOrdersByUserIdQueryResult>
    {
        public GetOrdersByUserIdQueryHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor) 
            : base(context, accessor)
        {
        }

        public async Task<GetOrdersByUserIdQueryResult> Handle(
            GetOrdersByUserIdQuery request, 
            CancellationToken cancellationToken)
        {
            var order = await Context.Orders
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
                .Where(x => 
                    x.CreatedBy == request.UserId 
                    && x.VenueId == request.VenueId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return new GetOrdersByUserIdQueryResult()
            {
                Orders = order.ToUserResults()
            };
        }
    }
}
