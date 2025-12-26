using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Extensions;
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
    public class PatchWorkerPhotoCommandHandler
        : BaseHandler, IPatchWorkerPhotoCommandHandler
    {
        public PatchWorkerPhotoCommandHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<ICommandResult> Handle(PatchWorkerPhotoCommand request, CancellationToken cancellationToken)
        {
            var worker = await Context.Workers
                .Include(x => x.Venues)
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (worker != null && worker.WorkerId == Principal.GetUserId())
            {
                worker.PhotoUri = request.PhotoUri;
            }

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = worker?.Id
            };
        }
    }
}