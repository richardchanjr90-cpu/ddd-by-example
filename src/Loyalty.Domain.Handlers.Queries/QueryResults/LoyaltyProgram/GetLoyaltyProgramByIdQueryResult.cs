using System;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProgram
{
    public class GetLoyaltyProgramByIdQueryResult
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPublished { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string ExternalProgramUri { get; set; }
    }
}