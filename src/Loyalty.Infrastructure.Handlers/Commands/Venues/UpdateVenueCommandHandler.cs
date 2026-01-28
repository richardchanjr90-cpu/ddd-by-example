using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities.ValueObject;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Venues;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Infrastructure.Handlers.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Loyalty.Infrastructure.Handlers.Commands.Venues
{
    public class UpdateVenueCommandHandler : BaseHandler, IUpdateVenueCommandHandler
    {
        private readonly IMediator mediator;

        public UpdateVenueCommandHandler(ILoyaltyTenantDbContext context, IMediator mediator, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(UpdateVenueCommand request, CancellationToken cancellationToken)
        {
            var venue = await Context.Venues
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
                venue.FullDescription = request.FullDescription;
                venue.WebSites = request.WebSites.ToCommaSeparatedStringOrNull();
                venue.WorkingHours = JsonConvert.SerializeObject(request.WorkingHours);
                venue.Phones = request.Phones.ToCommaSeparatedStringOrNull();
                venue.Address = request.Location?.Address;
                venue.City = request.Location?.City;
                venue.Latitude = request.Location?.Latitude ?? 0.0f;
                venue.Longitude = request.Location?.Longitude ?? 0.0f;
                venue.IsPublished = request.IsPublished;
                venue.SocialNetworks = new SocialNetworks()
                {
                    Vkontakte = request.SocialNetworks?.Vkontakte,
                    Facebook = request.SocialNetworks?.Facebook,
                    Instagram = request.SocialNetworks?.Instagram,
                };
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