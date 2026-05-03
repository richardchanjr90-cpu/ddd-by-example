using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.Orders;
using Loyalty.Core.Outbox.Entities.Services;
using Loyalty.Domain.Handlers.Notifications.LoyaltyProductGroups;
using Loyalty.Domain.Handlers.Notifications.Rate;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.Orders
{
    public class OrderUserRateGivenDomainEventHandler :
        INotificationHandler<OrderUserRateGivenDomainEvent>
    {
        private readonly IEventBusService eventBusService;

        public OrderUserRateGivenDomainEventHandler(IEventBusService eventBusService)
        {
            this.eventBusService = eventBusService;
        }

        public async Task Handle(OrderUserRateGivenDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var order = domainEvent.Order;

            await eventBusService.PersistEventAsync(new UpsertUserRateNotification
            {
                VenueId = order.VenueId,
                OrderId = order.Id,
                Rate = (int) domainEvent.Rate,
                UserId = order.CreatedBy
            });
        }
    }
}