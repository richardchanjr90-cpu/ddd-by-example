using System;
using Loyalty.Domain.Handlers.Queries.QueryResults.ProductGroup;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.ProductGroup
{
    public class GetProductGroupsByUserIdQuery : IRequest<GetProductGroupsByUserIdQueryResult>
    {
        public string UserId { get; set; }
    }
}