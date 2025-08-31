using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Data.Entities.Base;
using Loyalty.Data.Entities.Base.Interface;
using Loyalty.Data.Entities.Schema;

namespace Loyalty.Data.Entities
{
    [Table("VenueDetails", Schema = SchemaName.Loyalty)]
    public class VenueDetails : AuditableEntity, IArchivableEntity
    {
        [ForeignKey(nameof(Venue))]
        public long VenueId { get; set; }

        [Required]
        [MaxLength(4000)]
        public string FullDescription { get; set; }

        [Required]
        public string Phones { get; set; }

        public string WebSites { get; set; }

        [Required]
        public string WorkingHours { get; set; }

        [Required]
        public string PhotosUrl { get; set; }

        public Venue Venue { get; set; }

        public bool IsArchived { get; set; }
    }
}
