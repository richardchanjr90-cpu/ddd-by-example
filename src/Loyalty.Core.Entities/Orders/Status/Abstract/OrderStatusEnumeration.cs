using System;
using System.Collections.Generic;
using System.Linq;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Entities.SeedWork;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Core.Entities.Orders.Status.Abstract
{
    public abstract class OrderStatusEnumeration : Enumeration
    {
        protected OrderStatusEnumeration(int id, string name)
            : base(id, name)
        {
        }

        protected OrderStatusEnumeration(OrderStatus id, string name)
            : this((int)id, name)
        {
        }

        public static OrderStatusEnumeration From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new LoyaltyValidationException(
                    $"Possible values for OrderStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static IEnumerable<OrderStatusEnumeration> List() =>
            new[]
            {
                PlacedOrder.Placed, 
                StartedOrder.Started, 
                ReadyOrder.Ready, 
                FinishedOrder.Finished, 
                DeclinedByVenueOrder.DeclinedByVenue, 
                DeclinedByCustomerOrder.DeclinedByCustomer, 
                ForceDeclinedByCustomerOrder.ForceDeclinedByCustomer, 
                NotRedeemedOrder.NotRedeemed
            };

        public virtual void Set(Order order)
        {
            throw new LoyaltyValidationException("Impossible to change order to this state.", ErrorCode.ORDER_INVALID_STATE);
        }
    }
}
