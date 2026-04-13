using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Entities.Aggregates.LoyaltyPrograms;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Schema;
using Loyalty.Core.Entities.SeedWork.Interfaces;

namespace Loyalty.Core.Entities.Aggregates.Purchases
{
    public class Purchase : AuditableEntity, IAggregateRoot
    {
        [ForeignKey(nameof(LoyaltyProductGroup))]
        public long LoyaltyProductGroupId { get; set; }

        public long VenueId { get; set; }

        public long? ProductId { get; set; }

        public string UserId { get; set; }

        public decimal? Value { get; set; }

        public override long TenantId => VenueId;
    }
}
