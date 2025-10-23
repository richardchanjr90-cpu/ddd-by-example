using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Shared.Enums;
using Loyalty.Data.Entities.Base;
using Loyalty.Data.Entities.Base.Interface;
using Loyalty.Data.Entities.Schema;

namespace Loyalty.Data.Entities
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

        public bool IsArchived { get; set; }

        public bool IsPublished { get; set; }

        public bool IsApproved { get; set; }
    }
}
