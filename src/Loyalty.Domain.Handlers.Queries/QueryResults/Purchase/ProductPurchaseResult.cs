using Loyalty.Shared.Contracts.Enums;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Purchase
{
    public class ProductPurchaseResult
    {
        public long? Id { get; set; }

        public string Name { get; set; }

        public ProductIconType? Icon { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }
    }
}