using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Domain.Handlers.Queries.Queries.LoyaltyProgram;
using Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProgram;
using Loyalty.Infrastructure.DataAccess;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.Handlers.Queries.LoyaltyPrograms
{
    public class GetLoyaltyProgramsByUserIdQueryHandler 
        : BaseHandler, IRequestHandler<GetLoyaltyProgramsByUserIdQuery, GetLoyaltyProgramsByUserIdQueryResult>
    {
        public GetLoyaltyProgramsByUserIdQueryHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public Task<GetLoyaltyProgramsByUserIdQueryResult> Handle(GetLoyaltyProgramsByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}