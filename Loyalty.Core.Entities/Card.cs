using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Base.Interface;
using Loyalty.Core.Entities.Schema;

namespace Loyalty.Core.Entities
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