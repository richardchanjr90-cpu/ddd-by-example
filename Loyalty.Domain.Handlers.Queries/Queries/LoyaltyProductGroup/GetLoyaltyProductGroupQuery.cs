using Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.LoyaltyProductGroup
{
    public class GetLoyaltyProductGroupQuery : IRequest<GetLoyaltyProductGroupsQueryResult>
    {
    }
}