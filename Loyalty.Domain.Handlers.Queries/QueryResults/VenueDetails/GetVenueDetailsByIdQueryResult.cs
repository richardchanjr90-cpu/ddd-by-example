using System.Collections.Generic;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.VenueDetails
{
    public class GetVenueDetailsByIdQueryResult
    {
        public long Id { get; set; }

        public long VenueId { get; set; }

        public string FullDescription { get; set; }

        public List<string> Phones { get; set; } = new List<string>();

        public List<string> WebSites { get; set; } = new List<string>();

        public List<string> WorkingHours { get; set; } = new List<string>();

        public List<string> PhotosUrl { get; set; } = new List<string>();
    }
}
