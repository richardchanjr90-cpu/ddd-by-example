using System.Collections.Generic;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Worker
{
    public class GetInviteByEmailQueryResult
    {
        public long Id { get; set; }

        public List<GetVenueWorkerResult> Venues { get; set; } = new List<GetVenueWorkerResult>();

        public string Phone { get; set; }

        public string Name { get; set; }
    }
}