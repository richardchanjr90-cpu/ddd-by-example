using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Loyalty.Core.Entities.Base
{
    public abstract class TenantEntity : Entity
    {
        [NotMapped]
        public abstract long TenantId { get; }
    }
}
