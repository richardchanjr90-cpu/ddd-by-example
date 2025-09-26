using System;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Worker
{
    public class GetWorkerByIdQueryResult
    {
        public string Id { get; set; }

        public long VenueId { get; set; }

        public Guid WorkerId { get; set; }

        public int Role { get; set; }

        public string Phone { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhotoUri { get; set; }
    }
}
