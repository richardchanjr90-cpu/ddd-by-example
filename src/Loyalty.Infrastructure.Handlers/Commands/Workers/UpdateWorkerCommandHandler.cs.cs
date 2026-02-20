using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Shared.Contracts.Enums;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.Handlers.Commands.Workers
{
    public class UpdateWorkerCommandHandler
        : BaseHandler, IRequestHandler<UpdateWorkerCommand, ICommandResult>
    {
        public UpdateWorkerCommandHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<ICommandResult> Handle(UpdateWorkerCommand request, CancellationToken cancellationToken)
        {
            var worker = Context.Workers
                .Where(x => x.Id == request.Id)
                .AsEnumerable()
                .FirstOrDefault();

            if (request.Role == VenueUserRole.Owner)
            {
                throw new LoyaltyValidationException("Impossible to create a second owner.", null, ErrorCode.SECOND_OWNER_NOT_ALLOWED);
            }

            worker.WorkerId = request.WorkerId;
            worker.Name = request.Name;
            worker.LastName = request.LastName;
            worker.Phone = request.Phone;
            worker.Email = request.Email;
            worker.PhotoUri = request.PhotoUri;
            worker.PositionName = request.PositionName;
            worker.IsArchived = false;

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = worker.Id
            };
        }
    }
}