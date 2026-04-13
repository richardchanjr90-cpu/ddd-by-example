using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Notifications.LoyaltyProductGroups;
using Loyalty.Domain.Handlers.Notifications.LoyaltyPrograms;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Loyalty.Infrastructure.Handlers.Extensions;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Venues
{
    public class ArchiveVenueCommandHandler : BaseHandler, IRequestHandler<ArchiveVenueCommand, ICommandResult>
    {
        private readonly IMediator mediator;

        public ArchiveVenueCommandHandler(
            ILoyaltyTenantDbContext context, 
            IMediator mediator, 
            IHttpContextAccessor accessor)
            : base(context, accessor)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(ArchiveVenueCommand request, CancellationToken cancellationToken)
        {
            var venue = await Context.Venues
                .Include(x => x.LoyaltyPrograms)
                .ThenInclude(x => x.LoyaltyProductGroups)
                .Include(x => x.ProductGroups)
                .Include(x => x.Workers)
                .Where(x => x.OwnerId == request.OwnerId && x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (venue != null)
            {
                venue.IsArchived = true;

                foreach (var program in venue.LoyaltyPrograms)
                {
                    program.IsArchived = true;

                    foreach (var lpg in program.LoyaltyProductGroups)
                    {
                        lpg.IsArchived = true;
                    }
                }

                foreach (var group in venue.ProductGroups)
                {
                    group.Archive();
                }
            }

            var result = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = venue?.Id
            };

            if (result.Success && venue != null)
            {
                await mediator.Publish(venue.ToArchiveNotification(), cancellationToken);

                foreach (var program in venue.LoyaltyPrograms)
                {
                    await mediator.Publish(
                        new ArchiveLoyaltyProgramNotification
                        {
                            Id = program.Id
                        },
                        cancellationToken);

                    foreach (var lpg in program.LoyaltyProductGroups)
                    {
                        await mediator.Publish(
                            new ArchiveLoyaltyProductGroupNotification
                            {
                                Id = lpg.Id
                            },
                            cancellationToken);
                    }
                }
            }

            return result;
        }
    }
}
