using System;
using System.Collections.Generic;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Worker
{
    public class GetInviteByPhoneQueryResult
    {
        public long Id { get; set; }

        public List<long> VenueIds { get; set; } = new List<long>();

        public VenueUserRole Role { get; set; }

        public string Phone { get; set; }

        public string Name { get; set; }

        public string PositionName { get; set; }
    }
}