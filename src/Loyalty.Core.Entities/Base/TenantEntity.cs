using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Entities.Base.Interface;

namespace Loyalty.Core.Entities.Base
{
    public abstract class TenantEntity : AuditableEntity, ITenantEntity
    {
        [NotMapped]
        public abstract long TenantId { get; }
    }
}
