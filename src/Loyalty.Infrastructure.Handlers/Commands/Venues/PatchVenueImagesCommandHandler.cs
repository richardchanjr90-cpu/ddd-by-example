using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Extensions;
using Loyalty.Core.Contracts;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Notifications.Venue;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Venues
{
    public class PatchVenueImagesCommandHandler : BaseHandler, IRequestHandler<PatchVenueImagesCommand, ICommandResult>
    {
        private readonly IMediator mediator;

        public PatchVenueImagesCommandHandler(ILoyaltyTenantDbContext context, IMediator mediator)
            : base(context, null)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(PatchVenueImagesCommand request, CancellationToken cancellationToken)
        {
            var venue = await Context.Venues
                .IgnoreQueryFilters()
                .Where(x => x.Id == request.Id)
                .SingleAsync(cancellationToken);

            venue.Images = request.Images.ToCommaSeparatedStringOrNull();

            var result = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = venue.Id
            };

            if (result.Success)
            {
                await mediator.Publish(
                    new PatchVenueImagesNotification
                    {
                        Images = venue.Images,
                        Id = venue.Id
                    },
                    cancellationToken);
            }

            return result;
        }
    }
}
