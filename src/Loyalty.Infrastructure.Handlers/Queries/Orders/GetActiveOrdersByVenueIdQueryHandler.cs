using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Domain.Handlers.Queries.Queries.Orders;
using Loyalty.Domain.Handlers.Queries.QueryResults.Orders;
using Loyalty.Infrastructure.DataAccess;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.Handlers.Queries.Orders
{
    public class GetActiveOrdersByVenueIdQueryHandler      
        : BaseHandler, IRequestHandler<GetActiveOrdersByVenueIdQuery, GetOrdersByVenueIdQueryResult>
    {
        public GetActiveOrdersByVenueIdQueryHandler(ILoyaltyDbContext context, IHttpContextAccessor accessor) 
            : base(context, accessor)
        {
        }

        public Task<GetOrdersByVenueIdQueryResult> Handle(
            GetActiveOrdersByVenueIdQuery request, 
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
