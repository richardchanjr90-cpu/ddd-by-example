namespace Loyalty.Domain.Handlers.Queries.QueryResults.ProductGroup
{
    public class GetProductGroupByIdQueryResult
    {
        public string Id { get; set; }

        public long VenueId { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }
    }
}
