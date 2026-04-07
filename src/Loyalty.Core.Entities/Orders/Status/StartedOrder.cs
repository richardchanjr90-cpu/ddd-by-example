using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Entities.Orders.Status.Abstract;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Core.Entities.Orders.Status
{
    public class StartedOrder : OrderStatusEnumeration
    {
        public static readonly OrderStatusEnumeration Started =
            new StartedOrder(OrderStatus.Started, nameof(OrderStatus.Started).ToLowerInvariant());

        public StartedOrder(int id, string name) 
            : base(id, name)
        {
        }

        public StartedOrder(OrderStatus id, string name) 
            : base(id, name)
        {
        }

        public override void Set(Order order)
        {
            if (order.Status.Id > Started.Id)
            {
                throw new LoyaltyValidationException("Impossible to change order to this state.", ErrorCode.ORDER_INVALID_STATE);
            }

            order.UpdateStatus(this);
        }
    }
}
