using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Events.LoyaltyProductGroups;
using Loyalty.Core.Outbox.Entities.Services;
using Loyalty.Domain.Handlers.Notifications.LoyaltyProductGroups;
using MediatR;

namespace Loyalty.Application.DomainEvents.Handlers.LoyaltyProductGroups
{
    public class LoyaltyGroupUpdatedDomainEventHandler :
        INotificationHandler<LoyaltyGroupUpdatedDomainEvent>
    {
        private readonly IEventBusService eventBusService;

        public LoyaltyGroupUpdatedDomainEventHandler(IEventBusService eventBusService)
        {
            this.eventBusService = eventBusService;
        }

        public async Task Handle(LoyaltyGroupUpdatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var group = domainEvent.Group;

            var settings = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var rules = new List<object>();

            foreach (var rule in group.Rules)
            {
                var objectRule = new
                {
                    Rule = rule.Rule,
                    RuleType = rule.RuleType,
                    RuleVersion = rule.RuleVersion
                };

                rules.Add(objectRule);
            }

            await eventBusService.PersistEventAsync(new UpdateLoyaltyProductGroupNotification
            {
                Id = group.Id,
                GroupName = group.Name,
                Rule = JsonSerializer.Serialize(rules, settings)
            });
        }
    }
}