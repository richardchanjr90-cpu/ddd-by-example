using System;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Data.Entities.Base;
using Loyalty.Data.Entities.Schema;

namespace Loyalty.Data.Entities
{
    [Table("VenueDetails", Schema = SchemaName.Loyalty)]
    public class VenueDetails : AuditableEntity
    {
        [ForeignKey(nameof(Venue))]
        public long VenueId { get; set; }

        public string FullDescription { get; set; }

        public string Phones { get; set; }

        public string WebSites { get; set; }

        public string WorkingHours { get; set; }

        public string PhotosUrl { get; set; }
    }
}