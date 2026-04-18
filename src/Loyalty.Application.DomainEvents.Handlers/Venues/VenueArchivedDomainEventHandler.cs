using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.Venues;
using Loyalty.Core.Outbox.Entities.Services;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.Venues
{
    public class VenueArchivedDomainEventHandler :
        INotificationHandler<VenueArchivedDomainEvent>
    {
        private readonly IEventBusService eventBusService;

        public VenueArchivedDomainEventHandler(IEventBusService eventBusService)
        {
            this.eventBusService = eventBusService;
        }

        public async Task Handle(VenueArchivedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
