using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Shared.Enums;
using Loyalty.Data.Entities.Base;
using Loyalty.Data.Entities.Schema;

namespace Loyalty.Data.Entities
{
    [Table("Venue", Schema = SchemaName.Loyalty)]
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