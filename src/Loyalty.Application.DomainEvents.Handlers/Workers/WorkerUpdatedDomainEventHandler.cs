using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.Workers;
using Loyalty.Core.Outbox.Entities.Services;
using Loyalty.Domain.Handlers.Notifications.Workers;
using Loyalty.Shared.Contracts.Enums;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.Workers
{
    public class WorkerUpdatedDomainEventHandler :
        INotificationHandler<WorkerUpdatedDomainEvent>
    {
        private readonly IEventBusService eventBusService;

        public WorkerUpdatedDomainEventHandler(IEventBusService eventBusService)
        {
            this.eventBusService = eventBusService;
        }

        public async Task Handle(WorkerUpdatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            await eventBusService.PersistEventAsync( new UpdatedWorkerNotification
            {
                WorkerId = domainEvent.Worker.WorkerId,
                LastName = domainEvent.Worker.LastName,
                Name = domainEvent.Worker.Name,
                City = domainEvent.Worker.City
            });

            var roles = new Dictionary<string, VenueUserRole>();

            foreach (var venueRole in domainEvent.Worker.VenueRoles)
            {
                roles.Add(venueRole.VenueId.ToString(), venueRole.Role);
            }

            await eventBusService.PersistEventAsync( new SetupFirebaseTokenNotification
            {
                WorkerId = domainEvent.Worker.WorkerId,
                Surname = domainEvent.Worker.LastName,
                City = domainEvent.Worker.City,
                Name = domainEvent.Worker.Name,
                VenueRoles = roles
            });
        }
    }
}
