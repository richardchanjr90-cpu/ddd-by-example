using System;
using Loyalty.Data.Entities.Base;

namespace Loyalty.Data.Entities
{
    public class Stamp : AuditableEntity
    {
        public int CardId { get; set; }

        public DateTime StampedDate { get; set; }

        public DateTime? UsedDate { get; set; }
    }
}