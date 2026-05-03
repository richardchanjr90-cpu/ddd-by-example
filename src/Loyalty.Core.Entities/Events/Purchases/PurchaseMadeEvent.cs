using Loyalty.Core.Entities.Aggregates.Purchases;
using MediatR;

namespace Loyalty.Core.Entities.Events.Purchases
{
    public class PurchaseMadeEvent : INotification
    {
        public string WorkerId { get; private set; }

        public Purchase Purchase { get; private set; }

        public PurchaseMadeEvent(Purchase purchase, string workerId)
        {
            Purchase = purchase;
            WorkerId = workerId;
        }
    }
}
