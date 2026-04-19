using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.Orders;
using Loyalty.Core.Outbox.Entities.Services;
using Loyalty.Domain.Handlers.Notifications.Orders;
using Loyalty.Shared.Contracts.Enums;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.Orders
{
    public class OrderStatusChangedDomainEventHandler :
        INotificationHandler<OrderStatusChangedDomainEvent>
    {
        private readonly IEventBusService eventBusService;

        public OrderStatusChangedDomainEventHandler(IEventBusService eventBusService)
        {
            this.eventBusService = eventBusService;
        }

        public async Task Handle(OrderStatusChangedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var order = domainEvent.Order;

            await eventBusService.PersistEventAsync(new UpdateOrderNotification
            {
                Id = order.Id,
                Status = (OrderStatus)order.Status.Id,
                VenueId = order.VenueId
            });
        }
    }
}