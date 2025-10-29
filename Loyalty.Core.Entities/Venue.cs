using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Common.Shared.Enums.Contracts;
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

        [MaxLength(4000)]
        public string FullDescription { get; set; }

        public string Phones { get; set; }

        public string WebSites { get; set; }

        public string WorkingHours { get; set; }

        public virtual ICollection<LoyaltyProgram> LoyaltyPrograms { get; set; }

        public virtual ICollection<Worker> Workers { get; set; }

        public virtual ICollection<ProductGroup> ProductGroups { get; set; }

        public bool IsArchived { get; set; }

        public bool IsApproved { get; set; }

        public bool IsPublished { get; set; }
    }
}
