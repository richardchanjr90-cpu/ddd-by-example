using System;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.GeoPosition
{
    public class GetGeoPositionQueryResult
    {
        public Guid Id { get; set; }

        public string City { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }
    }
}
