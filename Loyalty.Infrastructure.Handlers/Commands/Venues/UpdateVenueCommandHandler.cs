using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Venues;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Infrastructure.Handlers.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Loyalty.Infrastructure.Handlers.Commands.Venues
{
    public class UpdateVenueCommandHandler : BaseHandler, IUpdateVenueCommandHandler
    {
        private readonly IMediator mediator;

        public UpdateVenueCommandHandler(ILoyaltyDbContext context, IMediator mediator)
            : base(context)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(UpdateVenueCommand request, CancellationToken cancellationToken)
        {
            var venue = await Context.Venues
                .Include(x => x.Location)
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
                venue.FullDescription = request.FullDescription;
                venue.WebSites = request.WebSites.ToCommaSeparatedStringOrNull();
                venue.WorkingHours = JsonConvert.SerializeObject(request.WorkingHours);
                venue.Phones = request.Phones.ToCommaSeparatedStringOrNull();
                venue.Location = request.Location?.ToSingle();
            }

            var result = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = venue.Id
            };

            if (result.Success)
            {
                await mediator.Publish(venue.ToUpdateNotification(), cancellationToken);
            }

            return result;
        }
    }
}