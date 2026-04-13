using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Domain.Handlers.Queries.Queries.Venue;
using Loyalty.Domain.Handlers.Queries.QueryResults.Venue;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Loyalty.Infrastructure.Handlers.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Queries.Venues
{
    public class GetVenueByIdQueryHandler : BaseHandler, IRequestHandler<GetVenueByIdQuery, GetVenueByIdQueryResult>
    {
        public GetVenueByIdQueryHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<GetVenueByIdQueryResult> Handle(GetVenueByIdQuery request,
            CancellationToken cancellationToken)
        {
            var venue = await Context.Venues
                .Where(x => x.Id == request.Id)
                .AsNoTracking()
                .SingleOrDefaultAsync(cancellationToken);

            return venue?.ToResult();
        }
    }
}