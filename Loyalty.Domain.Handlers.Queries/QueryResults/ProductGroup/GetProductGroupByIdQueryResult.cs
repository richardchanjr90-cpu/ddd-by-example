using System.Collections.Generic;
using Loyalty.Domain.Handlers.Queries.QueryResults.Product;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.ProductGroup
{
    public class GetProductGroupByIdQueryResult
    {
        public long Id { get; set; }

        public long VenueId { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }

        public List<GetProductByIdQueryResult> Products { get; set; } = new List<GetProductByIdQueryResult>();
    }
}
