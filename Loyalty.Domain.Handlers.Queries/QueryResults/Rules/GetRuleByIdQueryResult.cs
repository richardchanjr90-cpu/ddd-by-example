using System.Collections.Generic;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Rules
{
    public class GetRuleByIdQueryResult
    {
        public List<GetSingleRuleByIdQueryResult> Rules { get; set; } = new List<GetSingleRuleByIdQueryResult>();
    }
}