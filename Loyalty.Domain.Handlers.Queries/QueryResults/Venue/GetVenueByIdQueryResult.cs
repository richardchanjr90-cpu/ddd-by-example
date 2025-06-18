using System;
using Loyalty.Core.Shared.Enums;
using Loyalty.Domain.Handlers.Queries.QueryResults.GeoPosition;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Venue
{
    public class GetVenueByIdQueryResult
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid OwnerId { get; set; }

        public string Description { get; set; }

        public GetGeoPositionQueryResult Location { get; set; }

        public Guid? ParentId { get; set; }

        public VenueType Type { get; set; }

        public VenueCategory Category { get; set; }
    }
}
