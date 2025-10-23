using Loyalty.Domain.Handlers.Queries.Queries.ProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.ProductGroup;
using MediatR;

namespace Loyalty.Domain.Handlers.Contracts.Queries.ProductGroups
{
    public interface IGetProductGroupByIdQueryHandler : 
        IRequestHandler<GetProductGroupByIdQuery, GetProductGroupByIdQueryResult>
    {
    }
}
