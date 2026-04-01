using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Contracts.Commands.ProductGroups;
using Loyalty.Domain.Handlers.Queries.Commands.ProductGroups;
using Loyalty.Infrastructure.DataAccess;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;

namespace Loyalty.Infrastructure.Handlers.Commands.ProductGroups
{
    public class CreateProductGroupCommandHandler
        : BaseHandler, ICreateProductGroupCommandHandler
    {
        public CreateProductGroupCommandHandler(ILoyaltyTenantDbContext context, IHttpContextAccessor accessor)
            : base(context, accessor)
        {
        }

        public async Task<ICommandResult> Handle(CreateProductGroupCommand request, CancellationToken cancellationToken)
        {
            var group = new ProductGroup
            {
                VenueId = request.VenueId,
                Icon = request.Icon,
                Name = request.Name,
            };

            await Context.ProductGroups.AddAsync(group, cancellationToken);

            return new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = group.Id
            };
        }
    }
}