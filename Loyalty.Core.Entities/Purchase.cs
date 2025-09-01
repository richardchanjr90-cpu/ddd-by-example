using System;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Entities.Base;
using Loyalty.Core.Entities.Schema;

namespace Loyalty.Core.Entities
{
    [Table("Purchase", Schema = SchemaName.Loyalty)]
    public class Purchase : AuditableEntity
    {
        [ForeignKey(nameof(Card))]
        public long CardId { get; set; }

        public DateTime? BurnDate { get; set; }

        public decimal? Value { get; set; }
    }
}
