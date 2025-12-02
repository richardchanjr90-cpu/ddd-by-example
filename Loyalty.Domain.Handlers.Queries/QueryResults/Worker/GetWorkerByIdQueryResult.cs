using System;
using System.Collections.Generic;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Worker
{
    public class GetWorkerByIdQueryResult
    {
        public long Id { get; set; }

        public List<long> VenueIds { get; set; } = new List<long>();

        public string WorkerId { get; set; }

        public VenueUserRole Role { get; set; }

        public string Phone { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhotoUri { get; set; }

        public string PositionName { get; set; }
    }
}