using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Data.Entities.Base;
using Loyalty.Data.Entities.Base.Interface;
using Loyalty.Data.Entities.Schema;

namespace Loyalty.Data.Entities
{
    [Table("LoyaltyProgram", Schema = SchemaName.Loyalty)]
    public class LoyaltyProgram : AuditableEntity, IArchivableEntity, IRequireTwoStepSaveEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsArchived { get; set; }

        public bool IsPublished { get; set; }

        public virtual ICollection<LoyaltyProduct> LoyaltyProducts { get; set; }

        [ForeignKey(nameof(Venue))]
        public long VenueId { get; set; }
    }
}
