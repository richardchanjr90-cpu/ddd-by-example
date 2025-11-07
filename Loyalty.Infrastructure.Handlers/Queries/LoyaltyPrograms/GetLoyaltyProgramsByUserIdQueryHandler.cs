using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.LoyaltyPrograms;
using Loyalty.Domain.Handlers.Queries.Queries.LoyaltyProgram;
using Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProgram;

namespace Loyalty.Infrastructure.Handlers.Queries.LoyaltyPrograms
{
    public class GetLoyaltyProgramsByUserIdQueryHandler : BaseHandler, IGetLoyaltyProgramsByUserIdQueryHandler
    {
        public GetLoyaltyProgramsByUserIdQueryHandler(ILoyaltyDbContext context)
            : base(context)
        {
        }

        public Task<GetLoyaltyProgramsByUserIdQueryResult> Handle(GetLoyaltyProgramsByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}