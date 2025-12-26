using Loyalty.Domain.Handlers.Queries.Queries.LoyaltyProductGroup;
using Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProductGroup;
using MediatR;

namespace Loyalty.Domain.Handlers.Contracts.Queries.LoyaltyProductGroups
{
    public interface IGetLoyaltyProductGroupByIdQueryHandler
        : IRequestHandler<GetLoyaltyProductGroupByIdQuery, GetLoyaltyProductGroupByIdQueryResult>
    {
    }
}