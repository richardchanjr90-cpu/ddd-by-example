namespace Loyalty.Domain.Handlers.Queries.QueryResults.Product
{
    public class GetProductByIdQueryResult
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }

        public long ProductGroupId { get; set; }
    }
}
