using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.Workers;
using Loyalty.Core.Outbox.Entities.Services;
using Loyalty.Domain.Handlers.Notifications.Workers;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.Workers
{
    public class OwnerCreatedDomainEventHandler :
        INotificationHandler<OwnerCreatedDomainEvent>
    {
        private readonly IEventBusService eventBusService;

        public OwnerCreatedDomainEventHandler(IEventBusService eventBusService)
        {
            this.eventBusService = eventBusService;
        }

        public async Task Handle(OwnerCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            await eventBusService.PersistEventAsync(new UpdatedWorkerNotification
            {
                WorkerId = domainEvent.Worker.WorkerId,
                LastName = domainEvent.Worker.LastName,
                Name = domainEvent.Worker.Name,
                City = domainEvent.Worker.City
            });

            await eventBusService.PersistEventAsync(new SetupFirebaseTokenNotification
            {
                WorkerId = domainEvent.Worker.WorkerId,
                Surname = domainEvent.Worker.LastName,
                City = domainEvent.Worker.City,
                Name = domainEvent.Worker.Name,
            });
        }
    }
}
