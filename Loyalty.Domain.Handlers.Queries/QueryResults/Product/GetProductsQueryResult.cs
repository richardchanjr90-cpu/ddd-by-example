using System.Collections.Generic;

namespace Loyalty.Domain.Handlers.Queries.QueryResults.Product
{
    public class GetProductsQueryResult
    {
        public List<GetProductByIdQueryResult> Result { get; set; } 
            = new List<GetProductByIdQueryResult>();
    }
}
