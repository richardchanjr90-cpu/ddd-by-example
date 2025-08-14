using System;
using Loyalty.Data.Entities.Base;

namespace Loyalty.Data.Entities
{
    public class LoyaltyProgram : AuditableEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime StartedDate { get; set; }

        public DateTime EndedDate { get; set; }

        public bool IsArchived { get; set; }

        public bool CardBecomesInactiveAfterEnd { get; set; }

        public int VenueId { get; set; }
    }
}