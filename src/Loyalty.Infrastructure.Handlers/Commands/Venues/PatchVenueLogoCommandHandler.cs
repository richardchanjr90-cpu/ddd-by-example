using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Venues;
using Loyalty.Domain.Handlers.Notifications.Venue;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Venues
{
    public class PatchVenueLogoCommandHandler : BaseHandler, IPatchVenueLogoCommandHandler
    {
        private readonly IMediator mediator;

        public PatchVenueLogoCommandHandler(ILoyaltyTenantDbContext context, IMediator mediator, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(PatchVenueLogoCommand request, CancellationToken cancellationToken)
        {
            var venue = await Context.Venues
                .Where(x => x.Id == request.Id)
                .SingleAsync(cancellationToken);

            venue.LogoUrl = request.Logo;

            var result = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = venue.Id
            };

            if (result.Success)
            {
                await mediator.Publish(
                    new PatchVenueLogoNotification
                    {
                        Logo = request.SmallLogo,
                        Id = venue.Id
                    },
                    cancellationToken);
            }

            return result;
        }
    }
}
