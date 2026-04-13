using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Entities.Aggregates.Venues;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Base.Interface;
using Loyalty.Core.Entities.SeedWork.Interfaces;

namespace Loyalty.Core.Entities.Aggregates.LoyaltyPrograms
{
    public class LoyaltyProgram : AuditableEntity, IArchivableEntity, IAggregateRoot
    {
        [Required]
        public string Name { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime? EndDate { get; set; }

        public virtual ICollection<LoyaltyProductGroup> LoyaltyProductGroups { get; set; }

        [ForeignKey(nameof(Venue))]
        public long VenueId { get; set; }

        public Venue OwnerVenue { get; set; }

        public bool IsPublished { get; set; }

        public bool IsArchived { get; set; }

        public Uri Url { get; set; }

        public override long TenantId => VenueId;
    }
}
