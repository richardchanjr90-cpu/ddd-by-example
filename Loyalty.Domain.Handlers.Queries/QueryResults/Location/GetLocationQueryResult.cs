using System;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Location
{
    public class GetLocationQueryResult
    {
        public long Id { get; set; }

        public long VenueId { get; set; }

        public string City { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }
    }
}
