using Loyalty.Core.Entities.Aggregates.Orders.Status.Abstract;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Core.Entities.Aggregates.Orders.Status
{
    public class DeclinedByCustomerOrder : OrderStatusEnumeration
    {
        public static readonly OrderStatusEnumeration DeclinedByCustomer =
            new DeclinedByCustomerOrder(OrderStatus.DeclinedByCustomer, nameof(OrderStatus.DeclinedByCustomer).ToLowerInvariant());

        public DeclinedByCustomerOrder(int id, string name) 
            : base(id, name)
        {
        }

        public DeclinedByCustomerOrder(OrderStatus id, string name) 
            : base(id, name)
        {
        }
    }
}
