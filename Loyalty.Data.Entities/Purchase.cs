using System;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Data.Entities.Base;
using Loyalty.Data.Entities.Schema;

namespace Loyalty.Data.Entities
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
