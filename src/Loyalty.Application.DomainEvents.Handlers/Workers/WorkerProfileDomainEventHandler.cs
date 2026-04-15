using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.Workers;
using Loyalty.Core.Outbox.Entities.Services;
using Loyalty.Domain.Handlers.Notifications.Workers;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.Workers
{
    public class WorkerProfileDomainEventHandler :
        INotificationHandler<WorkerArchivedDomainEvent>
    {
        private readonly IEventBusService eventBusService;

        public WorkerProfileDomainEventHandler(IEventBusService eventBusService)
        {
            this.eventBusService = eventBusService;
        }

        public async Task Handle(WorkerArchivedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var worker = domainEvent.Worker;

            await eventBusService.PersistEventAsync(new UpdateWorkerProfileNotification
            {
                WorkerId = worker.WorkerId,
                LastName = worker.LastName,
                Name = worker.Name
            });
        }
    }
}
