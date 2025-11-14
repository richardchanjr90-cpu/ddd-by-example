namespace Loyalty.Domain.Handlers.Queries.QueryResults.Location
{
    public class GetLocationQueryResult
    {
        public string City { get; set; }

        public string Address { get; set; }

        public float? Latitude { get; set; }

        public float? Longitude { get; set; }
    }
}
