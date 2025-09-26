using Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProgram;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.LoyaltyProgram
{
    public class GetLoyaltyProgramsQuery : IRequest<GetLoyaltyProgramsQueryResult>
    {
        public long VenueId { get; set; }
    }
}