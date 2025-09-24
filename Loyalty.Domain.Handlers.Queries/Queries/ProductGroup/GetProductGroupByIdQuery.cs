using Loyalty.Domain.Handlers.Queries.QueryResults.ProductGroup;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.ProductGroup
{
    public class GetProductGroupByIdQuery : IRequest<GetProductGroupByIdQueryResult>
    {
        public long Id { get; set; }
    }
}