using Loyalty.Domain.Handlers.Queries.Queries.Product;
using Loyalty.Domain.Handlers.Queries.QueryResults.Product;
using MediatR;

namespace Loyalty.Domain.Handlers.Contracts.Queries.Products
{
    public interface IGetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdQueryResult>
    {
    }
}
