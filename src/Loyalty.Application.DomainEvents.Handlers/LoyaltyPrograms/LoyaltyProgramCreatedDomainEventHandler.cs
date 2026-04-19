using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.LoyaltyPrograms;
using Loyalty.Core.Outbox.Entities.Services;
using Loyalty.Domain.Handlers.Notifications.LoyaltyPrograms;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.LoyaltyPrograms
{
    public class LoyaltyProgramCreatedDomainEventHandler :
        INotificationHandler<LoyaltyProgramCreatedDomainEvent>
    {
        private readonly IEventBusService eventBusService;

        public LoyaltyProgramCreatedDomainEventHandler(IEventBusService eventBusService)
        {
            this.eventBusService = eventBusService;
        }

        public async Task Handle(LoyaltyProgramCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var program = domainEvent.Program;

            await eventBusService.PersistEventAsync(new CreateLoyaltyProgramNotification
            {
                Id = program.Id,
                VenueId = program.VenueId,
                Name = program.Name,
                EndDate = program.EndDate,
                StartDate = program.StartDate,
                IsPublished = program.IsPublished,
                Url = program.Url?.ToString(),
                Description = program.Description
            });
        }
    }
}