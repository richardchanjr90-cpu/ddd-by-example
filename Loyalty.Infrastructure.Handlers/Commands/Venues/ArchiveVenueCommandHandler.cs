using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Venues;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Infrastructure.Handlers.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Venues
{
    public class ArchiveVenueCommandHandler : BaseHandler, IArchiveVenueCommandHandler
    {
        private readonly IMediator mediator;

        public ArchiveVenueCommandHandler(ILoyaltyDbContext context, IMediator mediator)
            : base(context)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(ArchiveVenueCommand request, CancellationToken cancellationToken)
        {
            var venue = await Context.Venues
                .Include(x => x.Location)
                .Where(x => x.OwnerId == request.OwnerId && x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (venue != null)
            {
                if (venue.Location != null)
                {
                    venue.Location.IsArchived = true;
                }

                venue.IsArchived = true;
            }
            var result = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = venue?.Id
            };

            if (result.Success)
            {
                await mediator.Publish(venue.ToArchiveNotification(), cancellationToken);
            }

            return result;
        }
    }
}
