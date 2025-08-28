using System;
using System.Collections.Generic;
namespace Loyalty.Domain.Handlers.Queries.QueryResults.Venue
{
    public class GetVenuesQueryResult
    {
        public List<GetVenueByIdQueryResult> Venues { get; set; } = new List<GetVenueByIdQueryResult>();
    }
}
