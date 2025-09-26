using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Workers;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Workers
{
    public class UpdateWorkerCommandHandler
        : BaseHandler, IUpdateWorkerCommandHandler
    {
        public UpdateWorkerCommandHandler(ILoyaltyDbContext context) : base(context)
        {
        }

        public async Task<ICommandResult> Handle(UpdateWorkerCommand request, CancellationToken cancellationToken)
        {
            var worker = await Context.Workers
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (worker == null)
            {
                worker = new Worker
                {
                    WorkerId = request.WorkerId,
                    VenueId = request.VenueId,
                    Name = request.Name,
                    Email = request.Email,
                    LastName = request.LastName,
                    Phone = request.Phone,
                    PhotoUri = request.PhotoUri,
                    Role = request.Role
                };

                Context.Workers.Add(worker);
            }
            else
            {
                worker.WorkerId = request.WorkerId;
                worker.VenueId = request.VenueId;
                worker.Name = request.Name;
                worker.Email = request.Email;
                worker.LastName = request.LastName;
                worker.Phone = request.Phone;
                worker.PhotoUri = request.PhotoUri;
                worker.Role = request.Role;
            }

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = worker.Id
            };
        }
    }
}
