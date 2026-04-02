using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Notifications.Products;
using Loyalty.Domain.Handlers.Queries.Commands.Products;
using Loyalty.Infrastructure.DataAccess;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Products
{
    public class PatchProductImageCommandHandler
        : BaseHandler,  IRequestHandler<PatchProductImageCommand, ICommandResult>
    {
        private readonly IMediator mediator;

        public PatchProductImageCommandHandler(ILoyaltyTenantDbContext context, IMediator mediator,  IHttpContextAccessor accessor)
            : base(context, accessor)
        {
            this.mediator = mediator;
        }

        public async Task<ICommandResult> Handle(PatchProductImageCommand request, CancellationToken cancellationToken)
        {
            var product = await Context.Products
                .Include(x => x.ProductGroup)
                .Where(x => x.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            product?.SetImage(request.ImageUri);
            
            var result = new CommandResult
            {
                Success = await Context.SaveChangesAsync(cancellationToken) > 0,
                Result = product?.Id
            };

            if (product != null && result.Success)
            {
                await mediator.Publish(
                    new PatchProductImageNotification
                    {
                        Id = product.Id,
                        ImageUri = request.ImageUri != null ? new Uri(request.ImageUri) : null,
                    }, cancellationToken);
            }

            return result;
        }
    }
}