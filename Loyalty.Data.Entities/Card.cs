using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Data.Entities.Base;
using Loyalty.Data.Entities.Base.Interface;
using Loyalty.Data.Entities.Schema;

namespace Loyalty.Data.Entities
{
    [Table("Card", Schema = SchemaName.Loyalty)]
    public class Card : AuditableEntity, IArchivableEntity
    {
        [ForeignKey(nameof(LoyaltyProductGroup))]
        public long LoyaltyProductGroupId { get; set; }

        public Guid UserId { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }

        public bool IsArchived { get; set; }
    }
}