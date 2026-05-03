using Loyalty.Core.Entities.Base.Interface;
using Loyalty.Core.Entities.SeedWork;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Core.Entities.Aggregates.Workers
{
    public class VenueWorker : Entity, ITenantEntity
    {
        public VenueWorker(
            long venueId,
            long workerId,
            VenueUserRole role,
            string positionName)
        {
            VenueId = venueId;
            WorkerId = workerId;
            Role = role;
            PositionName = positionName;
        }

        public long VenueId { get; private set; }

        public long WorkerId { get; private set; }

        public VenueUserRole Role { get; private set; }

        public string PositionName { get; private set; }

        public long TenantId => VenueId;

        internal void UpdateRole(VenueUserRole role, string positionName)
        {
            Role = role;
            PositionName = positionName;
        }
    }
}
