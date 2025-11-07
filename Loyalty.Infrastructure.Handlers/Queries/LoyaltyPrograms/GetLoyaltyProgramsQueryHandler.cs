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
    public class GetLoyaltyProgramsQueryHandler : BaseHandler, IGetLoyaltyProgramsQueryHandler
    {
        public GetLoyaltyProgramsQueryHandler(ILoyaltyDbContext context)
            : base(context)
        {
        }

        public async Task<GetLoyaltyProgramsQueryResult> Handle(GetLoyaltyProgramsQuery request,
            CancellationToken cancellationToken)
        {
            var items = await (from lp in Context.LoyaltyPrograms
                where lp.VenueId == request.VenueId
                select new GetLoyaltyProgramByIdQueryResult
                {
                    Id = lp.Id,
                    IsArchived = lp.IsArchived,
                    Description = lp.Description,
                    StartedDate = lp.StartDate,
                    EndedDate = lp.EndDate,
                    Name = lp.Name,
                    IsPublished = lp.IsPublished
                }).ToListAsync(cancellationToken);

            return new GetLoyaltyProgramsQueryResult
            {
                Result = items
            };
        }
    }
}