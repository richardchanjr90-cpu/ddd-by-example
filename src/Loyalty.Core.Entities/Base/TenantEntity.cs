using System.ComponentModel.DataAnnotations.Schema;

namespace Loyalty.Core.Entities.Base
{
    public abstract class TenantEntity : Entity
    {
        [NotMapped]
        public abstract long TenantId { get; }
    }
}
