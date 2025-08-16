using System;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Location
{
    public class GetLocationQueryResult
    {
        public Guid Id { get; set; }

        public string City { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }
    }
}
