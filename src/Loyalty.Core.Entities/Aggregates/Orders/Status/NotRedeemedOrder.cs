using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Entities.Aggregates.Orders.Status.Abstract;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Core.Entities.Aggregates.Orders.Status
{
    public class NotRedeemedOrder : OrderStatusEnumeration
    {
        public static readonly OrderStatusEnumeration NotRedeemed =
            new NotRedeemedOrder(OrderStatus.NotRedeemed, nameof(OrderStatus.NotRedeemed).ToLowerInvariant());

        public NotRedeemedOrder(int id, string name) 
            : base(id, name)
        {
        }

        public NotRedeemedOrder(OrderStatus id, string name) 
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
