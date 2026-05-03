using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.Purchases;
using Loyalty.Core.Outbox.Entities.Services;
using Loyalty.Domain.Handlers.Notifications.Purchases;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.Purchases
{
    public class PurchaseMadeEventHandler :
        INotificationHandler<PurchaseMadeEvent>
    {
        private readonly IEventBusService eventBusService;

        public PurchaseMadeEventHandler(IEventBusService eventBusService)
        {
            this.eventBusService = eventBusService;
        }


        public async Task Handle(PurchaseMadeEvent domainEvent, CancellationToken cancellationToken)
        {
            var purchase = domainEvent.Purchase;
            var date = DateTime.Now.ToUniversalTime();

            await eventBusService.PersistEventAsync(
                new CreatePurchaseNotification
                {
                    VenueId = purchase.VenueId,
                    UserId = purchase.UserId,
                    LoyaltyProductGroupId = purchase.LoyaltyProductGroupId,
                    Total = purchase.Value,
                    When = date,
                    WorkerId = domainEvent.WorkerId
                });
        }
    }
}