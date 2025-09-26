using Loyalty.Domain.Handlers.Queries.Queries.LoyaltyProgram;
using Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProgram;
using MediatR;

namespace Loyalty.Domain.Handlers.Contracts.Queries.LoyaltyPrograms
{
    public interface IGetLoyaltyProgramsQueryHandler : 
        IRequestHandler<GetLoyaltyProgramsQuery, GetLoyaltyProgramsQueryResult>
    {
    }
}