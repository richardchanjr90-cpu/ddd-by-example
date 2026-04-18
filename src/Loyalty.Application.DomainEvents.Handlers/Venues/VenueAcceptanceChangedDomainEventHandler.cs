using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.Venues;
using Loyalty.Core.Outbox.Entities.Services;
using Loyalty.Domain.Handlers.Notifications.Venue;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.Venues
{
    public class VenueAcceptanceChangedDomainEventHandler :
        INotificationHandler<VenueAcceptanceChangedDomainEvent>
    {
        private readonly IEventBusService eventBusService;

        public VenueAcceptanceChangedDomainEventHandler(IEventBusService eventBusService)
        {
            this.eventBusService = eventBusService;
        }

        public async Task Handle(VenueAcceptanceChangedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var venue = domainEvent.Venue;

            await eventBusService.PersistEventAsync(new PatchOrderAcceptanceNotification
            {
                VenueId = venue.Id,
                Accept = venue.AcceptsOrders
            });
        }
    }
}
