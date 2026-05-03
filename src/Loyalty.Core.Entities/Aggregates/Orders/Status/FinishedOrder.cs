using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Entities.Aggregates.Orders.Status.Abstract;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Core.Entities.Aggregates.Orders.Status
{
    public class FinishedOrder : OrderStatusEnumeration
    {
        public static readonly OrderStatusEnumeration Finished =
            new FinishedOrder(OrderStatus.Finished, nameof(OrderStatus.Finished).ToLowerInvariant());

        public FinishedOrder(int id, string name) 
            : base(id, name)
        {
        }

        public FinishedOrder(OrderStatus id, string name) 
            : base(id, name)
        {
        }

        public override void Set(Order order)
        {
            if (order.Status.Id >= Finished.Id)
            {
                throw new LoyaltyValidationException("Impossible to change order to this state.", ErrorCode.ORDER_INVALID_STATE);
            }

            order.UpdateStatus(this);
        }
    }
}
