using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Entities.Aggregates.Orders.Status.Abstract;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Core.Entities.Aggregates.Orders.Status
{
    public class DeclinedByVenueOrder : OrderStatusEnumeration
    {
        public static readonly OrderStatusEnumeration DeclinedByVenue =
            new DeclinedByVenueOrder(OrderStatus.DeclinedByVenue, nameof(OrderStatus.DeclinedByVenue).ToLowerInvariant());

        public DeclinedByVenueOrder(int id, string name) 
            : base(id, name)
        {
        }

        public DeclinedByVenueOrder(OrderStatus id, string name) 
            : base(id, name)
        {
        }

        public override void Set(Order order)
        {
            if (order.Status.Id >= FinishedOrder.Finished.Id)
            {
                throw new LoyaltyValidationException("Impossible to change order to this state.", ErrorCode.ORDER_INVALID_STATE);
            }

            order.UpdateStatus(this);
        }
    }
}
