using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.Workers;
using Loyalty.Core.Outbox.Entities.Services;
using Loyalty.Domain.Handlers.Notifications.Workers;
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
                PhotoUri = domainEvent.Worker.PhotoUri,
                Role = domainEvent.Role,
                VenueId = domainEvent.VenueId
            });
        }
    }
}
