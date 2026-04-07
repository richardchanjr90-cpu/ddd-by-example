using Loyalty.Core.Entities.Orders.Status.Abstract;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Core.Entities.Orders.Status
{
    public class ForceDeclinedByCustomerOrder : OrderStatusEnumeration
    {
        public static readonly OrderStatusEnumeration ForceDeclinedByCustomer =
            new ForceDeclinedByCustomerOrder(OrderStatus.ForceDeclinedByCustomer, nameof(OrderStatus.ForceDeclinedByCustomer).ToLowerInvariant());

        public ForceDeclinedByCustomerOrder(int id, string name) 
            : base(id, name)
        {
        }

        public ForceDeclinedByCustomerOrder(OrderStatus id, string name) 
            : base(id, name)
        {
        }
    }
}
