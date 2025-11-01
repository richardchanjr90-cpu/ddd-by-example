using System;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Schema;

namespace Loyalty.Core.Entities
{
    [Table("Purchase", Schema = SchemaName.Loyalty)]
    public class Purchase : AuditableEntity
    {
        [ForeignKey(nameof(LoyaltyProductGroup))]
        public long LoyaltyProductGroupId { get; set; }

        public long? ProductId { get; set; }

        public Guid UserId { get; set; }

        public bool InternalPurchaseMadeBySystem { get; set; }

        public decimal? Value { get; set; }

        public DateTime? BurnDate { get; set; }
    }
}
