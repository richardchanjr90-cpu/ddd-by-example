using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Product
{
    public class GetProductByIdQueryResult
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public ProductIconType? Icon { get; set; }

        public long ProductGroupId { get; set; }
    }
}