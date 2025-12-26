using Loyalty.Domain.Handlers.Queries.QueryResults.ProductGroup;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.ProductGroup
{
    public class GetProductGroupsQuery : IRequest<GetProductGroupsQueryResult>
    {
        public long VenueId { get; set; }
    }
}