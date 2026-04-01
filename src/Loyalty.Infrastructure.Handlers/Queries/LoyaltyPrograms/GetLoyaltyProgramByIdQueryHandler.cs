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
    public class GetLoyaltyProgramByIdQueryHandler 
        : BaseHandler, IRequestHandler<GetLoyaltyProgramByIdQuery, GetLoyaltyProgramByIdQueryResult>
    {
        public GetLoyaltyProgramByIdQueryHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<GetLoyaltyProgramByIdQueryResult> Handle(GetLoyaltyProgramByIdQuery request,
            CancellationToken cancellationToken)
        {
            var item = await (from lp in Context.LoyaltyPrograms
                where lp.VenueId == request.VenueId && lp.Id == request.Id
                select new GetLoyaltyProgramByIdQueryResult
                {
                    Id = lp.Id,
                    Description = lp.Description,
                    StartedDate = lp.StartDate,
                    EndedDate = lp.EndDate,
                    Name = lp.Name,
                    IsPublished = lp.IsPublished
                }).SingleOrDefaultAsync(cancellationToken);

            return item;
        }
    }
}