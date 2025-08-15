using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Data.Entities.Base;
using Loyalty.Data.Entities.Schema;

namespace Loyalty.Data.Entities
{
    [Table("LoyaltyProgram", Schema = SchemaName.Loyalty)]
    public class LoyaltyProgram : AuditableEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime StartedDate { get; set; }

        public DateTime EndedDate { get; set; }

        public bool IsArchived { get; set; }

        public virtual ICollection<LoyaltyProgram> LoyaltyPrograms { get; set; }

        [ForeignKey(nameof(Venue))]
        public long VenueId { get; set; }
    }
}