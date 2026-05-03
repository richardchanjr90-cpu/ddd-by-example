using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.LoyaltyPrograms;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Core.Outbox.Entities.Services;
using Loyalty.Domain.Handlers.Notifications.LoyaltyPrograms;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.LoyaltyPrograms
{
    public class LoyaltyProgramArchivedDomainEventHandler :
        INotificationHandler<LoyaltyProgramArchivedDomainEvent>
    {
        private readonly IEventBusService eventBusService;

        public LoyaltyProgramArchivedDomainEventHandler(IEventBusService eventBusService)
        {
            this.eventBusService = eventBusService;
        }

        public async Task Handle(LoyaltyProgramArchivedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var program = domainEvent.Program;

            await eventBusService.PersistEventAsync(new ArchiveLoyaltyProgramNotification
            {
                Id = program.Id
            });
        }
    }
}