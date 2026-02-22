using System;
using System.Collections.Generic;
using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Worker
{
    public class GetInviteByPhoneQueryResult
    {
        public long Id { get; set; }

        public List<GetVenueWorkerResult> Venues { get; set; } = new List<GetVenueWorkerResult>();

        public string Phone { get; set; }

        public string Name { get; set; }
    }
}