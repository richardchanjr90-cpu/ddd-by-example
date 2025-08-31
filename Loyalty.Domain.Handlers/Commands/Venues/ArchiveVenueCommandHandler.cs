using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Shared.Settings;
using Loyalty.Data.Contracts;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Venues;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Loyalty.Domain.Handlers.Commands.Venues
{
    public class ArchiveVenueCommandHandler : BaseHandler, IArchiveVenueCommandHandler
    {
        public ArchiveVenueCommandHandler(ILoyaltyDbContext context)
            : base(context)
        {
        }

        public async Task<ICommandResult> Handle(ArchiveVenueCommand request, CancellationToken cancellationToken)
        {
            var venue = await Context.Venues
                .Include(x => x.VenueDetails)
                .Include(x => x.Location)
                .Where(x => x.OwnerId == request.OwnerId && x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (venue != null)
            {
                if (venue.VenueDetails != null)
                {
                    venue.VenueDetails.IsArchived = true;
                }

                if (venue.Location != null)
                {
                    venue.Location.IsArchived = true;
                }

                venue.IsArchived = true;
            }

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = venue?.Id
            };
        }
    }
}
