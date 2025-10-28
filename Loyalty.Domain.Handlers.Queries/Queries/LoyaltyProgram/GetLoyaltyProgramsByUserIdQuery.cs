using System;
using Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProgram;
using Loyalty.Domain.Handlers.Queries.QueryResults.ProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.Venue
{
    public class GetLoyaltyProgramsByUserIdQuery : IRequest<GetLoyaltyProgramsByUserIdQueryResult>
    {
        public Guid UserId { get; set; }
    }
}