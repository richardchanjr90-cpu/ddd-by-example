using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Entities.Aggregates.ProductGroups;
using Loyalty.Core.Entities.Aggregates.Purchases;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Base.Interface;

namespace Loyalty.Core.Entities.Aggregates.LoyaltyPrograms
{
    public class LoyaltyProductGroup : TenantEntity, IArchivableEntity
    {
        [ForeignKey(nameof(LoyaltyProgram))]
        public long LoyaltyProgramId { get; set; }

        public LoyaltyProgram LoyaltyProgram { get; set; }

        public string Name { get; set; }

        public virtual ICollection<LoyaltyGroupRule> Rules { get; set; }

        public ProductGroup Group { get; set; }

        [ForeignKey(nameof(ProductGroup))]
        public long ProductGroupId { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        public bool IsArchived { get; set; }

        public override long TenantId => Group.TenantId;
    }
}
