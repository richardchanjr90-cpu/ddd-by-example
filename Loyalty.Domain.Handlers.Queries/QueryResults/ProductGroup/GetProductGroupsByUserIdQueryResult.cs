using System.Collections.Generic;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.ProductGroup
{
    public class GetProductGroupsByUserIdQueryResult
    {
        public List<GetProductGroupByIdQueryResult> Result { get; set; }
            = new List<GetProductGroupByIdQueryResult>();
    }
}