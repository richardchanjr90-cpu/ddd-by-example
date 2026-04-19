using Loyalty.Core.Entities.Aggregates.Orders;
using Loyalty.Shared.Contracts.Enums;
using MediatR;

namespace Loyalty.Core.Entities.Events.Orders
{
    public class OrderUserRateGivenDomainEvent : INotification
    {
        public Order Order { get; private set; }

        public string Comment { get; private set; } 

        public OrderVenueRate Rate { get; private set; } 

        public OrderUserRateGivenDomainEvent(Order order, string comment, OrderVenueRate rate)
        {
            Comment = comment;
            Rate = rate;
            Order = order;
        }
    }
}
