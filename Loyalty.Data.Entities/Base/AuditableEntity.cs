using System;

namespace Loyalty.Data.Entities.Base
{
    public abstract class AuditableEntity : Entity
    {
        public Guid CreatedBy { get; set; }

        public Guid ModifiedBy { get; set; }

        public DateTime Modified { get; set; }

        public DateTime Created { get; set; }
    }
}
