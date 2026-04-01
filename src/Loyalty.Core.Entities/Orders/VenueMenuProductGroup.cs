using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Entities.Schema;

namespace Loyalty.Core.Entities.Orders
{
    [Table("VenueMenuProductGroup", Schema = SchemaName.Loyalty)]
    public class VenueMenuProductGroup
    {
        public long MenuId { get; set; }

        public VenueMenu Menu { get; set; }

        public long ProductGroupId { get; set; }

        public ProductGroup ProductGroup { get; set; }
    }
}
