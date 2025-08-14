using System;
using System.Collections.Generic;
using Loyalty.Core.Shared.Enums;
using Loyalty.Data.Entities.Base;

namespace Loyalty.Data.Entities
{
    public class Venue : AuditableEntity
    {
        public string Name { get; set; }

        public Guid OwnerId { get; set; }

        public string Description { get; set; }

        public GeoPosition Location { get; set; }

        public Guid? ParentId { get; set; }

        public VenueType Type { get; set; }

        public VenueCategory Category { get; set; }

        public IEnumerable<Venue> Subsidiaries { get; set; }
    }
}