using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Shared.Settings;
using Loyalty.Data.Contracts;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Venues;
using Loyalty.Domain.Handlers.Extensions;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Loyalty.Domain.Handlers.Commands.Venues
{
    public class UpdateVenueCommandHandler : BaseHandler, IUpdateVenueCommandHandler
    {
        public UpdateVenueCommandHandler(ILoyaltyDbContext context, IOptions<DbSettings> settings)
            : base(context)
        {
        }

        public async Task<ICommandResult> Handle(UpdateVenueCommand request, CancellationToken cancellationToken)
        {
            var venue = await Context.Venues
                .Include(x => x.Location)
                .Include(x => x.VenueDetails)
                .Where(x => x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (venue == null)
            {
                venue = request.ToSingle();
                Context.Venues.Add(venue);
            }
            else
            {
                venue.CategoryType = request.CategoryType;
                venue.Description = request.Description;
                venue.Name = request.Name;
                venue.Type = request.Type;
                venue.LogoUrl = request.LogoUrl;
                //todo: multistep implementation needed;
                venue.Location = request.Location?.ToSingle();
            }

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = venue.Id        
            };
        }
    }
}
