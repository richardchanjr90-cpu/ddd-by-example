using System;
using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Data.Entities.Base;

namespace Loyalty.Data.Entities
{
    public class Purchase : AuditableEntity
    {
        [ForeignKey(nameof(Card))]
        public long CardId { get; set; }

        public DateTime? BurnDate { get; set; }

        public decimal? Price { get; set; }
    }
}
