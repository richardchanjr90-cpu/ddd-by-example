using System.Collections.Generic;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProgram
{
    public class GetLoyaltyProgramsByUserIdQueryResult
    {
        public List<GetLoyaltyProgramByIdQueryResult> Result { get; set; } 
            = new List<GetLoyaltyProgramByIdQueryResult>();
    }
}
