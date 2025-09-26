using Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProductGroup;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.LoyaltyProductGroup
{
    public class GetLoyaltyProductGroupQuery : IRequest<GetLoyaltyProductGroupsQueryResult>
    {
    }
}