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
    public class LoyaltyGroupCreatedDomainEventHandler :
        INotificationHandler<LoyaltyGroupCreatedDomainEvent>
    {
        private readonly IEventBusService eventBusService;

        public LoyaltyGroupCreatedDomainEventHandler(IEventBusService eventBusService)
        {
            this.eventBusService = eventBusService;
        }

        public async Task Handle(LoyaltyGroupCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var group = domainEvent.Group;

            var settings = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var rules = new List<object>();

            foreach (var rule in group.Rules)
            {
                object deserializedRule = JsonSerializer.Deserialize<object>(rule.Rule);

                var objectRule = new
                {
                    Rule = deserializedRule,
                    RuleType = rule.RuleType,
                    RuleVersion = rule.RuleVersion
                };

                rules.Add(objectRule);
            }

            await eventBusService.PersistEventAsync(new CreateLoyaltyProductGroupNotification
            {
                Id = group.Id,
                LoyaltyProgramId = group.LoyaltyProgramId,
                GroupName = group.Name,
                Rule = JsonSerializer.Serialize(rules, settings)
            });
        }
    }
}