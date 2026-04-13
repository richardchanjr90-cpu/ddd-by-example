using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Entities.Aggregates.Orders.Status.Abstract;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Core.Entities.Aggregates.Orders.Status
{
    public class ReadyOrder : OrderStatusEnumeration
    {
        public static readonly OrderStatusEnumeration Ready =
            new ReadyOrder(OrderStatus.Ready, nameof(OrderStatus.Placed).ToLowerInvariant());

        public ReadyOrder(int id, string name) 
            : base(id, name)
        {
        }

        public ReadyOrder(OrderStatus id, string name) 
            : base(id, name)
        {
        }

        public override void Set(Order order)
        {
            if (order.Status.Id > Ready.Id)
            {
                throw new LoyaltyValidationException("Impossible to change order to this state.", ErrorCode.ORDER_INVALID_STATE);
            }

            order.UpdateStatus(this);
        }
    }
}
