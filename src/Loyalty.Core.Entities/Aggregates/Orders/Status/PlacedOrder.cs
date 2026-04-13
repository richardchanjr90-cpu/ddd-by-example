using Loyalty.Core.Entities.Aggregates.Orders.Status.Abstract;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Core.Entities.Aggregates.Orders.Status
{
    public class PlacedOrder : OrderStatusEnumeration
    {
        public static readonly OrderStatusEnumeration Placed =
            new PlacedOrder(OrderStatus.Placed, nameof(OrderStatus.Placed).ToLowerInvariant());

        public PlacedOrder(int id, string name) 
            : base(id, name)
        {
        }

        public PlacedOrder(OrderStatus id, string name) 
            : base(id, name)
        {
        }
    }
}
