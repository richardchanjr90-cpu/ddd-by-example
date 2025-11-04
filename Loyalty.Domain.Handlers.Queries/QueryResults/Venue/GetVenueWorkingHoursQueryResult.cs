using System;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Venue
{
    public class GetVenueWorkingHoursQueryResult
    {
        public string Day { get; set; }

        public int? From { get; set; }

        public int? To { get; set; }
    }
}
