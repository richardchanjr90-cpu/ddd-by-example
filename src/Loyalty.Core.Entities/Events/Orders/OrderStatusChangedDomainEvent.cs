using Loyalty.Core.Entities.Aggregates.Orders;
using MediatR;

namespace Loyalty.Core.Entities.Events.Orders
{
    public class OrderStatusChangedDomainEvent : INotification
    {
        public Order Order { get; private set; }

        public OrderStatusChangedDomainEvent(Order order)
        {
            Order = order;
        }
    }
}
