using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Domain.Handlers.Queries.Queries.LoyaltyProgram;
using Loyalty.Domain.Handlers.Queries.QueryResults.LoyaltyProgram;
using Loyalty.Infrastructure.DataAccess;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Queries.LoyaltyPrograms
{
    public class GetLoyaltyProgramsQueryHandler 
        : BaseHandler, IRequestHandler<GetLoyaltyProgramsQuery, GetLoyaltyProgramsQueryResult>
    {
        public GetLoyaltyProgramsQueryHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<GetLoyaltyProgramsQueryResult> Handle(
            GetLoyaltyProgramsQuery request,
            CancellationToken cancellationToken)
        {
            var items = await (from lp in Context.LoyaltyPrograms
                where lp.VenueId == request.VenueId
                select new GetLoyaltyProgramByIdQueryResult
                {
                    Id = lp.Id,
                    Description = lp.Description,
                    StartedDate = lp.StartDate,
                    EndedDate = lp.EndDate,
                    Name = lp.Name,
                    Url = lp.Url,
                    IsPublished = lp.IsPublished
                }).ToListAsync(cancellationToken);

            return new GetLoyaltyProgramsQueryResult
            {
                Result = items
            };
        }
    }
}