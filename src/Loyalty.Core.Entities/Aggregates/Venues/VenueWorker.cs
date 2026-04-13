using Loyalty.Core.Entities.Aggregates.Workers;
using Loyalty.Core.Entities.Base;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Core.Entities.Aggregates.Venues
{
    public class VenueWorker : TenantEntity
    {
        public long VenueId { get; set; }

        public Venue Venue { get; set; }

        public long WorkerId { get; set; }

        public Worker Worker { get; set; }

        public VenueUserRole Role { get; set; }

        public string PositionName { get; set; }

        public override long TenantId => VenueId;
    }
}
