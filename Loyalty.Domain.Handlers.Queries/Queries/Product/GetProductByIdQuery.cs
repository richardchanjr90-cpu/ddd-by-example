using Loyalty.Domain.Handlers.Queries.QueryResults.Product;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.Product
{
    public class GetProductByIdQuery : IRequest<GetProductByIdQueryResult>
    {
        public long Id { get; set; }
    }
}