using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.LoyaltyPrograms;
using Loyalty.Domain.Handlers.Queries.Queries.LoyaltyProgram;
using Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProgram;

namespace Loyalty.Infrastructure.Handlers.Queries.LoyaltyPrograms
{
    public class GetLoyaltyProgramsQueryHandler : BaseHandler, IGetLoyaltyProgramsQueryHandler
    {
        public GetLoyaltyProgramsQueryHandler(ILoyaltyDbContext context) : base(context)
        {
        }

        public async Task<GetLoyaltyProgramsQueryResult> Handle(GetLoyaltyProgramsQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}