using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Contracts.Commands.ProductGroups;
using Loyalty.Domain.Handlers.Queries.Commands.ProductGroups;
using Loyalty.Infrastructure.DataAccess;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.ProductGroups
{
    public class ArchiveProductGroupCommandHandler
        : BaseHandler, IArchiveProductGroupCommandHandler
    {
        public ArchiveProductGroupCommandHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<ICommandResult> Handle(
            ArchiveProductGroupCommand request,
            CancellationToken cancellationToken)
        {
            var group = await Context.ProductGroups
                .Include(x => x.Products)
                .Include(x => x.LoyaltyProductGroups)
                .Where(x => x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (group != null)
            {
                if (group.LoyaltyProductGroups.Any(x => x.ProductGroupId == group.Id && !x.IsArchived))
                {
                    throw new LoyaltyValidationException("Cannot delete product group that is assigned to a loyalty group.", ErrorCode.INCORRECT_PRODUCT_GROUP);
                }

                group.IsArchived = true;

                if (group.Products != null)
                {
                    foreach (var product in group.Products)
                    {
                        product.Archive();
                    }
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