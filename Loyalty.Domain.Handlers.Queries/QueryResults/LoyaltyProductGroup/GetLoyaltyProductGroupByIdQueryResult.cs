namespace Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProductGroup
{
    public class GetLoyaltyProductGroupByIdQueryResult
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int RuleType { get; set; }

        public string RuleValue { get; set; }

        public bool IsArchived { get; set; }
    }
}
