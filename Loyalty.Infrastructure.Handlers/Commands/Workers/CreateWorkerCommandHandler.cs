using System;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.Workers;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;

namespace Loyalty.Infrastructure.Handlers.Commands.Workers
{
    public class CreateWorkerCommandHandler
        : BaseHandler, ICreateWorkerCommandHandler
    {
        public CreateWorkerCommandHandler(ILoyaltyDbContext context) 
            : base(context)
        {
        }

        public async Task<ICommandResult> Handle(CreateWorkerCommand request, CancellationToken cancellationToken)
        {
            var worker = new Worker
            {
                WorkerId = request.WorkerId,
                VenueId = request.VenueId,
                Name = request.Name,
                Email = request.Email,
                LastName = request.LastName,
                Phone = request.Phone,
                PositionName = request.PositionName,
                PhotoUri = request.PhotoUri,
                Role = request.Role              
            };

            Context.Workers.Add(worker);

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = worker.Id
            };
        }
    }
}
