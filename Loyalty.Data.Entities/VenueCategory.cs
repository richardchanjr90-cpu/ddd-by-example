using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Loyalty.Core.Shared.Enums;
using Loyalty.Data.Entities.Base;
using Loyalty.Data.Entities.Schema;

namespace Loyalty.Data.Entities
{
    [Table("VenueCategory", Schema = SchemaName.Loyalty)]
    public class VenueCategory : AuditableEntity
    {
        [ForeignKey(nameof(Venue))]
        public long VenueId { get; set; }

        public VenueCategoryType CategoryType { get; set; }
    }
}
