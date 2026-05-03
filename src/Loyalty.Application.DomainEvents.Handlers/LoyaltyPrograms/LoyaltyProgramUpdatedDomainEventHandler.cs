using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.LoyaltyPrograms;
using Loyalty.Core.Outbox.Entities.Services;
using Loyalty.Domain.Handlers.Notifications.LoyaltyPrograms;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.LoyaltyPrograms
{
    public class LoyaltyProgramUpdatedDomainEventHandler :
        INotificationHandler<LoyaltyProgramUpdatedDomainEvent>
    {
        private readonly IEventBusService eventBusService;

        public LoyaltyProgramUpdatedDomainEventHandler(IEventBusService eventBusService)
        {
            this.eventBusService = eventBusService;
        }

        public async Task Handle(LoyaltyProgramUpdatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var program = domainEvent.Program;

            await eventBusService.PersistEventAsync(new UpdateLoyaltyProgramNotification
            {
                Id = program.Id,
                Name = program.Name,
                Url = program.Url?.ToString(),
                EndDate = program.EndDate,
                StartDate = program.StartDate,
                IsPublished = program.IsPublished,
                Description = program.Description
            });
        }
    }
}