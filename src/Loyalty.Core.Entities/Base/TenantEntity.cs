using System.ComponentModel.DataAnnotations.Schema;
using Loyalty.Core.Entities.SeedWork;

namespace Loyalty.Core.Entities.Base
{
    public abstract class TenantEntity : Entity
    {
        [NotMapped]
        public abstract long TenantId { get; }
    }
}
