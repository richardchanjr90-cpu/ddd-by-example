using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.Purchases;
using Loyalty.Core.Outbox.Entities.Services;
using Loyalty.Domain.Handlers.Notifications.Products;
using Loyalty.Domain.Handlers.Notifications.Purchases;
using Loyalty.Domain.Handlers.Notifications.Visit;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.Purchases
{
    public class PurchaseBurnedEventHandler :
        INotificationHandler<PurchaseBurnedEvent>
    {
        private readonly IEventBusService eventBusService;

        public PurchaseBurnedEventHandler(IEventBusService eventBusService)
        {
            this.eventBusService = eventBusService;
        }

        public async Task Handle(PurchaseBurnedEvent domainEvent, CancellationToken cancellationToken)
        {
            var purchase = domainEvent.Purchase;
            var date = DateTime.Now.ToUniversalTime();

            await eventBusService.PersistEventAsync(
                new BurnPurchaseNotification
                {
                    VenueId = purchase.VenueId,
                    UserId = purchase.UserId,
                    LoyaltyProductGroupId = purchase.LoyaltyProductGroupId,
                    Total = purchase.Value,
                    When = date,
                    WorkerId = domainEvent.WorkerId
                });

            await eventBusService.PersistEventAsync(
                new CreateVisitNotification
                {
                    VenueId = purchase.VenueId,
                    UserId = purchase.UserId,
                    When = date
                });
        }
    }
}