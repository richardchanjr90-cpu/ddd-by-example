using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Common.Shared.Enums;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Base.Interface;
using Loyalty.Core.Entities.Schema;

namespace Loyalty.Core.Entities
{
    [Table("Venue", Schema = SchemaName.Loyalty)]
    public class Venue : AuditableEntity, 
        IRequireTwoStepSaveEntity, 
        IArchivableEntity, 
        IRequireApprovalEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        public long? ParentId { get; set; }

        public Location Location { get; set; }

        [Required]
        public VenueType Type { get; set; }

        public VenueCategoryType CategoryType { get; set; }

        [MaxLength(200)]
        public string LogoUrl { get; set; }

        public VenueDetails VenueDetails { get; set; }

        public virtual ICollection<LoyaltyProgram> LoyaltyPrograms { get; set; }

        public virtual ICollection<Worker> Workers { get; set; }

        public virtual ICollection<ProductGroup> ProductGroups { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public bool IsArchived { get; set; }

        public bool IsApproved { get; set; }

        public bool IsPublished { get; set; }
    }
}
