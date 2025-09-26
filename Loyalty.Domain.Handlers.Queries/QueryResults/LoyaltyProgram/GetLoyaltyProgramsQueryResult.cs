using System.Collections.Generic;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProgram
{
    public class GetLoyaltyProgramsQueryResult
    {
        public List<GetLoyaltyProgramByIdQueryResult> Result { get; set; } 
            = new List<GetLoyaltyProgramByIdQueryResult>();
    }
}
