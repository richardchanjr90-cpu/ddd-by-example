using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.Workers;
using Loyalty.Core.Outbox.Entities.Services;
using Loyalty.Domain.Handlers.Notifications.Products;
using Loyalty.Domain.Handlers.Notifications.Workers;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.Workers
{
    public class WorkerArchivedDomainEventHandler :
        INotificationHandler<WorkerArchivedDomainEvent>
    {
        private readonly IEventBusService eventBusService;

        public WorkerArchivedDomainEventHandler(IEventBusService eventBusService)
        {
            this.eventBusService = eventBusService;
        }

        public async Task Handle(WorkerArchivedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            await eventBusService.PersistEventAsync(new ArchiveWorkerNotification
            {
                WorkerId = domainEvent.Worker.WorkerId,
                VenueId = domainEvent.VenueId
            });
        }
    }
}
