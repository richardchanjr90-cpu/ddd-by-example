using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Data.Entities.Base;
using Loyalty.Data.Entities.Base.Interface;
using Loyalty.Data.Entities.Schema;

namespace Loyalty.Data.Entities
{
    [Table("LoyaltyProgram", Schema = SchemaName.Loyalty)]
    public class LoyaltyProgram : AuditableEntity, IArchivableEntity
    {
        [Required]
        public string Name { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public LoyaltyProgramRule LoyaltyRule { get; set; }

        public virtual ICollection<LoyaltyProductGroup> LoyaltyGroups { get; set; }

        [ForeignKey(nameof(Venue))]
        public long VenueId { get; set; }

        public bool IsArchived { get; set; }

        public bool IsPublished { get; set; }
    }
}
