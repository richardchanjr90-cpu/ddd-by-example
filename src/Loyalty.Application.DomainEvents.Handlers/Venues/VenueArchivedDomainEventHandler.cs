using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.Venues;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Core.Outbox.Entities.Services;
using Loyalty.Domain.Handlers.Notifications.Venue;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.Venues
{
    public class VenueArchivedDomainEventHandler :
        INotificationHandler<VenueArchivedDomainEvent>
    {
        private readonly IEventBusService eventBusService;
        private readonly ILoyaltyProgramRepository loyaltyProgramRepository;

        public VenueArchivedDomainEventHandler(
            IEventBusService eventBusService, 
            ILoyaltyProgramRepository loyaltyProgramRepository)
        {
            this.eventBusService = eventBusService;
            this.loyaltyProgramRepository = loyaltyProgramRepository;
        }

        public async Task Handle(VenueArchivedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            await eventBusService.PersistEventAsync(new ArchiveVenueNotification
            {
                Id = domainEvent.Venue.Id,
                OwnerId = domainEvent.Venue.OwnerId
            });

            var programs = await loyaltyProgramRepository.GetByVenueAsync(
                domainEvent.Venue.Id, 
                cancellationToken);

            foreach (var program in programs)
            {
                program.Archive();
                loyaltyProgramRepository.Update(program);
            }
        }
    }
}
