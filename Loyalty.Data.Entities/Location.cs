using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Data.Entities.Base;
using Loyalty.Data.Entities.Schema;

namespace Loyalty.Data.Entities
{
    [Table("Location", Schema = SchemaName.Loyalty)]
    public class Location : AuditableEntity
    {
        [ForeignKey(nameof(Venue))]
        public long VenueId { get; set; }

        public string City { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }
    }
}