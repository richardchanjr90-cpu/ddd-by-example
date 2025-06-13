using Loyalty.Data.Entities.Base;

namespace Loyalty.Data.Entities
{
    public class LoyaltyProduct : AuditableEntity
    {
        public string Name { get; set; }

        public bool IsArchived { get; set; }

        public int StampsToCollectCount { get; set; }
    }
}