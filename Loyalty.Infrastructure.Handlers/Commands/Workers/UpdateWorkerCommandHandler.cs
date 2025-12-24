using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Workers;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;
using Loyalty.Shared.Contracts.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Workers
{
    public class UpdateWorkerCommandHandler
        : BaseHandler, IUpdateWorkerCommandHandler
    {
        public UpdateWorkerCommandHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<ICommandResult> Handle(UpdateWorkerCommand request, CancellationToken cancellationToken)
        {
            var worker = await Context.Workers
                .Include(x => x.Venues)
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            foreach (var venueId in request.VenueIds)
            {
                var venue = worker.Venues.SingleOrDefault(x => x.VenueId == venueId);

                if (request.Role == VenueUserRole.Owner && venue.Role != VenueUserRole.Owner)
                {
                    throw new LoyaltyValidationException("Impossible to create second owner.", null, ErrorCode.SECOND_OWNER_NOT_ALLOWED);
                }
            }

            if (worker == null)
            {
                worker = new Worker
                {
                    WorkerId = request.WorkerId,
                    Name = request.Name,
                    Email = request.Email,
                    LastName = request.LastName,
                    Phone = request.Phone,
                    PhotoUri = request.PhotoUri,
                    PositionName = request.PositionName
                };

                foreach (var id in request.VenueIds)
                {
                    var venueWorker = new VenueWorker();
                    venueWorker.VenueId = id;
                    venueWorker.Worker = worker;
                    Context.VenueWorkers.Add(venueWorker);
                }

                Context.Workers.Add(worker);
            }
            else
            {
                worker.WorkerId = request.WorkerId;
                worker.Name = request.Name;
                worker.Email = request.Email;
                worker.LastName = request.LastName;
                worker.Phone = request.Phone;
                worker.PositionName = request.PositionName;
            }

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = worker.Id
            };
        }
    }
}