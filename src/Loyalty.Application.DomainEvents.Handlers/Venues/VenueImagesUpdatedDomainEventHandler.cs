using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.Venues;
using Loyalty.Core.Outbox.Entities.Services;
using Loyalty.Domain.Handlers.Notifications.Venue;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.Venues
{
    public class VenueImagesUpdatedDomainEventHandler :
        INotificationHandler<VenueImagesUpdatedDomainEvent>
    {
        private readonly IEventBusService eventBusService;

        public VenueImagesUpdatedDomainEventHandler(IEventBusService eventBusService)
        {
            this.eventBusService = eventBusService;
        }

        public async Task Handle(VenueImagesUpdatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var venue = domainEvent.Venue;

            await eventBusService.PersistEventAsync(new PatchVenueImagesNotification
            {
                Images = venue.Images,
                Id = venue.Id
            });
        }
    }
}
