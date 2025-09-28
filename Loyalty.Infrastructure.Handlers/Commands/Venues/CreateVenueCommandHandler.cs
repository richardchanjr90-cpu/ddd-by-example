using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Enums;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Venues;
using Loyalty.Domain.Handlers.Queries.Commands.Venue;
using Loyalty.Infrastructure.Handlers.Extensions;
using MediatR;

namespace Loyalty.Infrastructure.Handlers.Commands.Venues
{
    public class CreateVenueCommandHandler : BaseHandler, ICreateVenueCommandHandler
    {
        public CreateVenueCommandHandler(ILoyaltyDbContext context)
            : base(context)
        {
        }

        public async Task<ICommandResult> Handle(CreateVenueCommand request, CancellationToken cancellationToken)
        {
            var venue = request.ToSingle();

            //todo: fill owner with real details;
            var worker = new Worker();
            worker.WorkerId = request.OwnerId;
            worker.Role = VenueUserRole.Owner;
            worker.PositionName = "Owner";
            worker.Name = "NameStub";
            worker.LastName = "LastNameStub";

            venue.Workers = new List<Worker>();
            venue.Workers.Add(worker);

            Context.Venues.Add(venue);

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = venue.Id
            };
        }
    }
}