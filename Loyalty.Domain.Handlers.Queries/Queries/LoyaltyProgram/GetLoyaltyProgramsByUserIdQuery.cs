using System;
using Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProgram;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.LoyaltyProgram
{
    public class GetLoyaltyProgramsByUserIdQuery : IRequest<GetLoyaltyProgramsByUserIdQueryResult>
    {
        public Guid UserId { get; set; }
    }
}