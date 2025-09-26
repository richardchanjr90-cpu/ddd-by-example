using System.Collections.Generic;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProductGroup
{
    public class GetLoyaltyProductGroupsQueryResult
    {
        public List<GetLoyaltyProductGroupByIdQueryResult> Result { get; set; } 
            = new List<GetLoyaltyProductGroupByIdQueryResult>();
    }
}
