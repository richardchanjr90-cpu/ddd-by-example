using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            if (request.Role == VenueUserRole.Owner)
            {
                throw new ValidationException("Impossible to create second owner.");
            }

            var worker = await Context.Workers
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

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
                    venueWorker.Role = request.Role;
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
                worker.PhotoUri = request.PhotoUri;
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