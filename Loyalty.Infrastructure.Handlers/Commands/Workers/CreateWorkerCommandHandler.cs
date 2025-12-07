using System.ComponentModel.DataAnnotations;
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

namespace Loyalty.Infrastructure.Handlers.Commands.Workers
{
    public class CreateWorkerCommandHandler
        : BaseHandler, ICreateWorkerCommandHandler
    {
        public CreateWorkerCommandHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<ICommandResult> Handle(CreateWorkerCommand request, CancellationToken cancellationToken)
        {
            if (request.Role == VenueUserRole.Owner)
            {
                throw new ValidationException("Impossible to create second owner.");
            }

            var worker = new Worker
            {
                WorkerId = request.WorkerId,
                Name = request.Name,
                Email = request.Email,
                LastName = request.LastName,
                Phone = request.Phone,
                PositionName = request.PositionName,
            };

            foreach (var id in request.VenueIds)
            {
                var venueWorker = new VenueWorker();
                venueWorker.VenueId = id;
                venueWorker.Worker = worker;
                venueWorker.Role = request.Role;
                Context.VenueWorkers.Add(venueWorker);
            }

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = worker.Id
            };
        }
    }
}