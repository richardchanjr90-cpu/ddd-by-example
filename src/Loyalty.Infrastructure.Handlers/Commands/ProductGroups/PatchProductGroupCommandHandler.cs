using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.ProductGroups;
using Loyalty.Infrastructure.DataAccess;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.ProductGroups
{
    public class PatchProductGroupCommandHandler
        : BaseHandler, IRequestHandler<PatchProductGroupCommand, ICommandResult>
    {
        public PatchProductGroupCommandHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<ICommandResult> Handle(PatchProductGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await Context.ProductGroups
                .Include(x => x.Products)
                .Where(x => x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            foreach (var product in group.Products)
            {
                if (request.IsAvailableForOrder)
                {
                    product.ShowToCustomer();
                }
                else
                {
                    product.HideFromCustomer();
                }
            }

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = group?.Id
            };
        }
    }
}