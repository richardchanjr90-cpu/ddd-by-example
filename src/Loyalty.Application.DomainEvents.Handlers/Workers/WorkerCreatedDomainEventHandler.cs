using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.Workers;
using Loyalty.Core.Outbox.Entities.Services;
using Loyalty.Domain.Handlers.Notifications.Products;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.Workers
{
    public class WorkerCreatedDomainEventHandler :
        INotificationHandler<WorkerCreatedDomainEvent>
    {
        private readonly IEventBusService eventBusService;

        public WorkerCreatedDomainEventHandler(IEventBusService eventBusService)
        {
            this.eventBusService = eventBusService;
        }

        public async Task Handle(WorkerCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
