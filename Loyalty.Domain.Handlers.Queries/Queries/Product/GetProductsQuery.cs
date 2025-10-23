using Loyalty.Domain.Handlers.Queries.QueryResults.Product;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.Product
{
    public class GetProductsQuery : IRequest<GetProductsQueryResult>
    {
        public long ProductGroupId { get; set; }
    }
}
