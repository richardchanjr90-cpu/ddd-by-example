using System;
using Loyalty.Domain.Handlers.Queries.QueryResults.Orders;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.Orders
{
    public class GetOrdersByUserIdQuery : IRequest<GetOrdersByUserIdQueryResult>
    {
        public string UserId { get; set; }

        public long VenueId { get; set; }
    }
}
