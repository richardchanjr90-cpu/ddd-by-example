using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Data.Entities.Base;
using Loyalty.Data.Entities.Schema;
using Microsoft.Build.Framework;

namespace Loyalty.Data.Entities
{
    [Table("Location", Schema = SchemaName.Loyalty)]
    public class Location : AuditableEntity
    {
        [ForeignKey(nameof(Venue))]
        public long VenueId { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public float Latitude { get; set; }

        [Required]
        public float Longitude { get; set; }
    }
}