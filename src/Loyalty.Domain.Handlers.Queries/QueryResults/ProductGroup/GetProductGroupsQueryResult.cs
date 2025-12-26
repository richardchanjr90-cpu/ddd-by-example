using System.Collections.Generic;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.ProductGroup
{
    public class GetProductGroupsQueryResult
    {
        public List<GetProductGroupByIdQueryResult> Result { get; set; }
            = new List<GetProductGroupByIdQueryResult>();
    }
}