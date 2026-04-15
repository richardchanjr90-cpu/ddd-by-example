using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Domain.Handlers.Queries.Queries.UserProfile;
using Loyalty.Domain.Handlers.Queries.QueryResults.UserProfile;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Queries.UserProfile
{
    public class GetUserProfileByIdQueryHandler 
        : BaseHandler, IRequestHandler<GetUserProfileByIdQuery, GetUserProfileByIdQueryResult>
    {
        public GetUserProfileByIdQueryHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<GetUserProfileByIdQueryResult> Handle(
            GetUserProfileByIdQuery request,
            CancellationToken cancellationToken)
        {
            var worker = await Context.Workers
                .IgnoreQueryFilters()
                .Include(x => x.VenueRoles)
                .Where(x => x.WorkerId == request.UserId)
                .AsNoTracking()
                .SingleAsync(cancellationToken);

            var result = new GetUserProfileByIdQueryResult()
            {
                PhotoUri = worker.PhotoUri,
                Email = worker.Email,
                LastName = worker.LastName,
                Name = worker.Name,
                Phone = worker.Phone,
            };

            return result;
        }
    }
}