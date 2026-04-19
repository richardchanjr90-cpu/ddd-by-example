using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.LoyaltyProductGroups;
using Loyalty.Core.Entities.Events.LoyaltyPrograms;
using Loyalty.Core.Outbox.Entities.Services;
using Loyalty.Domain.Handlers.Notifications.LoyaltyProductGroups;
using Loyalty.Domain.Handlers.Notifications.LoyaltyPrograms;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.LoyaltyProductGroups
{
    public class LoyaltyGroupArchivedDomainEventHandler :
        INotificationHandler<LoyaltyGroupArchivedDomainEvent>
    {
        private readonly IEventBusService eventBusService;

        public LoyaltyGroupArchivedDomainEventHandler(IEventBusService eventBusService)
        {
            this.eventBusService = eventBusService;
        }

        public async Task Handle(LoyaltyGroupArchivedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var group = domainEvent.Group;

            await eventBusService.PersistEventAsync(new ArchiveLoyaltyProductGroupNotification
            {
                Id = group.Id
            });
        }
    }
}