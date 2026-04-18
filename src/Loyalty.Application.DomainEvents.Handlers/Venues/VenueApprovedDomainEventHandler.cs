using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.Venues;
using Loyalty.Core.Outbox.Entities.Services;
using Loyalty.Domain.Handlers.Notifications.Venue;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.Venues
{
    public class VenueApprovedDomainEventHandler :
        INotificationHandler<VenueApprovedDomainEvent>
    {
        private readonly IEventBusService eventBusService;

        public VenueApprovedDomainEventHandler(IEventBusService eventBusService)
        {
            this.eventBusService = eventBusService;
        }

        public async Task Handle(VenueApprovedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var venue = domainEvent.Venue;

            await eventBusService.PersistEventAsync(new PatchVenueApproveNotification
            {
                Id = venue.Id
            });
        }
    }
}
