using System;
using Loyalty.Shared.Contracts.Enums;
using LoyaltyClient.Domain.Handlers.Notifications.Base;

namespace LoyaltyClient.Domain.Handlers.Notifications.Interaction
{
    public class InteractionNotification : IClientVenueNotification
    {
        public string UserId { get; set; }

        public UserInteractionType InteractionType { get; set; }

        public DateTime When { get; set; }

        public long VenueId { get; set; }
    }
}
