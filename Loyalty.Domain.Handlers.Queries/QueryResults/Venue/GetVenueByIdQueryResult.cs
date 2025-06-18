using System;
using Loyalty.Core.Shared.Enums;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Venue
{
    public class GetVenueByIdQueryResult
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid OwnerId { get; set; }

        public string Description { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public string City { get; set; }

        public Guid? ParentId { get; set; }

        public VenueType Type { get; set; }

        public VenueCategory Category { get; set; }
    }
}
