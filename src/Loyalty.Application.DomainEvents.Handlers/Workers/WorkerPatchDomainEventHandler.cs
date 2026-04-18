using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.Workers;
using Loyalty.Core.Outbox.Entities.Services;
using Loyalty.Domain.Handlers.Notifications.Workers;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.Workers
{
    public class WorkerPatchDomainEventHandler :
        INotificationHandler<WorkerPatchDomainEvent>
    {
        private readonly IEventBusService eventBusService;

        public WorkerPatchDomainEventHandler(IEventBusService eventBusService)
        {
            this.eventBusService = eventBusService;
        }

        public async Task Handle(WorkerPatchDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            await eventBusService.PersistEventAsync(new PatchWorkerNotification
            {
                WorkerId = domainEvent.Worker.WorkerId,
                PhotoUri = domainEvent.Worker.PhotoUri
            });
        }
    }
}
