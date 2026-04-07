using System;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Entities.Orders.Status.Abstract;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Core.Entities.Orders.Status
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
