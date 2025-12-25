using Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProductGroup;
using MediatR;

namespace Loyalty.Domain.Handlers.Queries.Queries.LoyaltyProductGroup
{
    public class GetLoyaltyProductGroupByIdQuery : IRequest<GetLoyaltyProductGroupByIdQueryResult>
    {
        public long Id { get; set; }

        public long ProgramId { get; set; }
    }
}