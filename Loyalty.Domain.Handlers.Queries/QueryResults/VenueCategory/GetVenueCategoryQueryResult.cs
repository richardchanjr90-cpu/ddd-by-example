using Loyalty.Core.Shared.Enums;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.VenueCategory
{
    public class GetVenueCategoryQueryResult
    {
        public long Id { get; set; }

        public VenueCategoryType CategoryType { get; set; }
    }
}
