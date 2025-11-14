using System;
using MediatR;

namespace Loyalty.Domain.Handlers.Notifications.Purchases
{
    public class BurnPurchaseNotification : INotification
    {
        public long LoyaltyProductGroupId { get; set; }

        public Guid UserId { get; set; }

        public long VenueId { get; set; }

        public decimal? Total { get; set; }
    }
}