using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.Venues;
using Loyalty.Core.Outbox.Entities.Services;
using Loyalty.Domain.Handlers.Notifications.Venue;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.Venues
{
    public class VenueLogoUpdatedDomainEventHandler :
        INotificationHandler<VenueLogoUpdatedDomainEvent>
    {
        private readonly IEventBusService eventBusService;

        public VenueLogoUpdatedDomainEventHandler(IEventBusService eventBusService)
        {
            this.eventBusService = eventBusService;
        }

        public async Task Handle(VenueLogoUpdatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var venue = domainEvent.Venue;

            await eventBusService.PersistEventAsync(new PatchVenueLogoNotification()
            {
                Logo = venue.LogoUrl,
                Id = venue.Id
            });
        }
    }
}
