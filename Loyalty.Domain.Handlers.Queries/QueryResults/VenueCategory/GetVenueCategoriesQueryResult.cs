using System;
using System.Collections.Generic;
using System.Text;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.VenueCategory
{
    public class GetVenueCategoriesQueryResult
    {
        public List<GetVenueCategoryQueryResult> Result { get; set; } = default;
    }
}
