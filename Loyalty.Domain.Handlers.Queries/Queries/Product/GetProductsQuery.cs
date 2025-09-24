using Loyalty.Domain.Handlers.Queries.QueryResults.Product;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.Product
{
    public class GetProductsQuery : IRequest<GetProductsQueryResult>
    {
    }
}