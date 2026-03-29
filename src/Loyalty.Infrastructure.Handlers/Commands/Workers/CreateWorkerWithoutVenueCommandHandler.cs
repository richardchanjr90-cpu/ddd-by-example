using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;
using Loyalty.Infrastructure.DataAccess;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.Handlers.Commands.Workers
{
    public class CreateWorkerWithoutVenueCommandHandler
        : BaseHandler, IRequestHandler<CreateWorkerWithoutVenueCommand, ICommandResult>
    {
        private readonly IMediator mediator;

        public CreateWorkerWithoutVenueCommandHandler(
            ILoyaltyTenantDbContext context, 
            IHttpContextAccessor accessor,
            IMediator mediator)
            : base(context, accessor)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(CreateWorkerWithoutVenueCommand request, CancellationToken cancellationToken)
        {
            var worker = new Worker
            {
                WorkerId = request.WorkerId,
                Name = request.Name,
                LastName = request.LastName,
                Phone = request.Phone,
                Email = request.Email
            };

            await Context.Workers.AddAsync(worker, cancellationToken);

            var commandResult = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = worker.Id
            };

            return commandResult;
        }
    }
}