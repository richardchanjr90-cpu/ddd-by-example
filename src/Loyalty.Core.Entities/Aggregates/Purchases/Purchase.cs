using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Entities.Aggregates.LoyaltyPrograms;
using Loyalty.Core.Entities.Aggregates.Venues;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Events.Purchases;
using Loyalty.Core.Entities.SeedWork.Interfaces;

namespace Loyalty.Core.Entities.Aggregates.Purchases
{
    public class Purchase : TenantEntity, IAggregateRoot
    {
        private Purchase(
            string workerId,
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

            if (value > 0)
            {
                AddDomainEvent(new PurchaseMadeEvent(this, workerId));
            }
            else if (value < 0)
            {
                AddDomainEvent(new PurchaseBurnedEvent(this, workerId));
            }
        }

        private Purchase()
        {
            //for ef core
        }

        public static Purchase Burn(
            string workerId,
            long loyaltyProductGroupId,
            long venueId,
            long? productId,
            string userId,
            decimal? value)
        {
            var purchase = new Purchase(
                workerId,
                loyaltyProductGroupId, 
                venueId, 
                productId, 
                userId, 
                -value);

            return purchase;
        }

        public static Purchase Assign(
            string workerId,
            long loyaltyProductGroupId,
            long venueId,
            long? productId,
            string userId,
            decimal? value)
        {
            var purchase = new Purchase(
                workerId,
                loyaltyProductGroupId, 
                venueId, 
                productId, 
                userId, 
                value);

            return purchase;
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