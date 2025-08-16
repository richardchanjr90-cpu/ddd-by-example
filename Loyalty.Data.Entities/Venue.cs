using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Shared.Enums;
using Loyalty.Data.Entities.Base;
using Loyalty.Data.Entities.Base.Interface;
using Loyalty.Data.Entities.Schema;

namespace Loyalty.Data.Entities
{
    [Table("Venue", Schema = SchemaName.Loyalty)]
    public class Venue : AuditableEntity, IRequireTwoStepSaveEntity, IArchivableEntity
    {
        public string Name { get; set; }

        public Guid OwnerId { get; set; }

        public string Description { get; set; }

        public Location Location { get; set; }

        public bool IsPublished { get; set; }

        public VenueType Type { get; set; }

        public VenueCategory Category { get; set; }

        public string LogoUrl { get; set; }

        public VenueDetails VenueDetails { get; set; }

        public virtual ICollection<LoyaltyProgram> LoyaltyPrograms { get; set; }

        public bool IsArchived { get; set; }
    }
}