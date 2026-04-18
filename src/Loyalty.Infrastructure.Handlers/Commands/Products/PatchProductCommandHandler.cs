using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Notifications.Products;
using Loyalty.Domain.Handlers.Queries.Commands.Products;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Products
{
    public class PatchProductCommandHandler
        : IRequestHandler<PatchProductCommand, ICommandResult>
    {
        private readonly IProductRepository productRepository;

        public PatchProductCommandHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<ICommandResult> Handle(PatchProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository
                .GetAsync(request.Id, cancellationToken);

            if (request.IsAvailableForOrder)
            {
                product?.ShowToCustomer();
            }
            else
            {
                product?.HideFromCustomer();
            }

            productRepository.Update(product);

            var result = new CommandResult
            {
                Success = await productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken),
                Result = product?.Id
            };

            return result;
        }
    }
}