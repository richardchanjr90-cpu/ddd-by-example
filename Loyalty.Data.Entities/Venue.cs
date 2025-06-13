using System;
using Loyalty.Data.Contracts;
using Loyalty.Data.Entities.Base;

namespace Loyalty.Data.Entities
{
    public class Venue : AuditableEntity
    {
        public string Name { get; set; }

        public Guid OwnerId { get; set; }

        public string Description { get; set; }

        public GeoPosition Location { get; set; }

        public int? ParentId { get; set; }

        public VenueType Type { get; set; }
    }
}