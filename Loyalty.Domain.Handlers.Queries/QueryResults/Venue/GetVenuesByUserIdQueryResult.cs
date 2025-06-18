using System;
using System.Collections.Generic;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Venue
{
    public class GetVenuesByUserIdQueryResult
    {
        public List<GetVenueByIdQueryResult> Venues { get; set; } = new List<GetVenueByIdQueryResult>();
    }
}
