using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.VenueDetails
{
    public class GetVenueFullByIdQueryResult
    {
        public GetVenueDetailsByIdQueryResult Details { get; set; }

        public GetVenueByIdQueryResult Venue { get; set; }
    }
}
