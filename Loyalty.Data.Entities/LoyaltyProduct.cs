using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Data.Entities.Base;
using Loyalty.Data.Entities.Schema;

namespace Loyalty.Data.Entities
{
    [Table("LoyaltyProduct", Schema = SchemaName.Loyalty)]
    public class LoyaltyProduct : AuditableEntity
    {
        [ForeignKey(nameof(LoyaltyProgram))]
        public long LoyaltyProgramId { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

        public int DisplayType { get; set; }

        public string Description { get; set; }

        public bool IsArchived { get; set; }
    }
}