using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Schema;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Core.Entities
{
    [Table("VenueWorker", Schema = SchemaName.Loyalty)]
    public class VenueWorker : Entity
    {
        public long VenueId { get; set; }

        public Venue Venue { get; set; }

        public long WorkerId { get; set; }

        public Worker Worker { get; set; }

        public VenueUserRole Role { get; set; }
    }
}
