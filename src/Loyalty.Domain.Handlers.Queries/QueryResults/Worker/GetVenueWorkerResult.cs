using System;
using System.Collections.Generic;
using System.Text;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Worker
{
    public class GetVenueWorkerResult
    {
        public long VenueId { get; set; }

        public string PositionName { get; set; }

        public VenueUserRole Role { get; set; }
    }
}
