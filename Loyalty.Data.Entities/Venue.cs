using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Shared.Enums;
using Loyalty.Data.Entities.Base;
using Loyalty.Data.Entities.Base.Interface;
using Loyalty.Data.Entities.Schema;
using Microsoft.Build.Framework;

namespace Loyalty.Data.Entities
{
    [Table("Venue", Schema = SchemaName.Loyalty)]
    public class Venue : AuditableEntity, IRequireTwoStepSaveEntity, IArchivableEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public string Description { get; set; }

        public long? FranchiseId { get; set; }

        [Required]
        public Location Location { get; set; }

        public bool IsPublished { get; set; }

        [Required]
        public VenueType Type { get; set; }

        public string LogoUrl { get; set; }

        public VenueDetails VenueDetails { get; set; }

        public virtual ICollection<LoyaltyProgram> LoyaltyPrograms { get; set; }

        public virtual ICollection<VenueCategory> Categories { get; set; }

        public bool IsArchived { get; set; }
    }
}