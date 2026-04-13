using Loyalty.Core.Entities.Base.Interface;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Core.Entities.Aggregates.Venues
{
    public class VenueWorker : ITenantEntity
    {
        public long VenueId { get; set; }

        public long WorkerId { get; set; }

        public VenueUserRole Role { get; set; }

        public string PositionName { get; set; }

        public long TenantId => VenueId;
    }
}
