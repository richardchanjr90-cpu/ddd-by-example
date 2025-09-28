using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Handlers.Contracts.Queries.LoyaltyPrograms;
using Loyalty.Domain.Handlers.Queries.Queries.LoyaltyProgram;
using Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProgram;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Queries.LoyaltyPrograms
{
    public class GetLoyaltyProgramByIdQueryHandler : BaseHandler, IGetLoyaltyProgramByIdQueryHandler
    {
        public GetLoyaltyProgramByIdQueryHandler(ILoyaltyDbContext context) 
            : base(context)
        {
        }

        public async Task<GetLoyaltyProgramByIdQueryResult> Handle(GetLoyaltyProgramByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await (from lp in Context.LoyaltyPrograms
                where lp.VenueId == request.VenueId && lp.Id == request.Id
                select new GetLoyaltyProgramByIdQueryResult
                {
                    Id = lp.Id,
                    IsArchived = lp.IsArchived,
                    Description = lp.Description,
                    StartedDate = lp.StartDate,
                    EndedDate = lp.EndDate,
                    Name = lp.Name,
                    IsPublished = lp.IsPublished
                }).SingleAsync(cancellationToken);

            return item;
        }
    }
}
