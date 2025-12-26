using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Contracts;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Contracts.Commands.ProductGroups;
using Loyalty.Domain.Handlers.Queries.Commands.ProductGroups;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.ProductGroups
{
    public class UpdateProductGroupCommandHandler
        : BaseHandler, IUpdateProductGroupCommandHandler
    {
        public UpdateProductGroupCommandHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<ICommandResult> Handle(UpdateProductGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await Context.ProductGroups
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (group != null)
            {
                group.VenueId = request.VenueId;
                group.Icon = request.Icon;
                group.Name = request.Name;
            }
            else
            {
                group = new ProductGroup
                {
                    VenueId = request.VenueId,
                    Icon = request.Icon,
                    Name = request.Name
                };
                Context.ProductGroups.Update(group);
            }

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = group?.Id
            };
        }
    }
}