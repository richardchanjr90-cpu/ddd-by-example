using System;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProgram
{
    public class GetLoyaltyProgramByIdQueryResult
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPublished { get; set; }

        public DateTime? StartedDate { get; set; }

        public DateTime? EndedDate { get; set; }

        public bool IsArchived { get; set; }
    }
}
