using Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProgram;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.LoyaltyProgram
{
    public class GetLoyaltyProgramByIdQuery : IRequest<GetLoyaltyProgramByIdQueryResult>
    {
        public long Id { get; set; }

        public long VenueId { get; set; }
    }
}