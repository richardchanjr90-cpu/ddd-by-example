using System;
using Loyalty.Domain.Handlers.Queries.QueryResults.Orders;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.Orders
{
    public class GetAllOrdersByVenueIdQuery : IRequest<GetOrdersByVenueIdQueryResult>
    {
    }
}
