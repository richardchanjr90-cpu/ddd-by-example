using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Entities.Aggregates.LoyaltyPrograms;
using Loyalty.Core.Entities.Aggregates.Venues;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.SeedWork.Interfaces;

namespace Loyalty.Core.Entities.Aggregates.Purchases
{
    public class Purchase : AuditableEntity, IAggregateRoot
    {
        public Purchase(
            long loyaltyProductGroupId,
            long venueId,
            long? productId,
            string userId,
            decimal? value)
        {
            LoyaltyProductGroupId = loyaltyProductGroupId;
            VenueId = venueId;
            ProductId = productId;
            UserId = userId;
            Value = value;
        }

        private Purchase()
        {
            //for ef core
        }

        [ForeignKey(nameof(LoyaltyProductGroup))]
        public long LoyaltyProductGroupId { get; private set; }

        [ForeignKey(nameof(Venue))]
        public long VenueId { get; private set; }

        public long? ProductId { get; private set; }

        public string UserId { get; private set; }

        public decimal? Value { get; private set; }

        public override long TenantId => VenueId;
    }
}