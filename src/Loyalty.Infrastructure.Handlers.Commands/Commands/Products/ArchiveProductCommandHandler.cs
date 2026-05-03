using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.Products;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Infrastructure.Handlers.Commands.Commands.Products
{
    public class ArchiveProductCommandHandler : IRequestHandler<ArchiveProductCommand, ICommandResult>
    {
        private readonly IProductRepository productRepository;

        public ArchiveProductCommandHandler(
            IProductRepository productRepository)
        { 
            this.productRepository = productRepository;
        }

        public async Task<ICommandResult> Handle(ArchiveProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository
                .GetAsync(request.Id, cancellationToken);

            product?.Archive();

            if (product != null)
            {
                productRepository.Update(product);
            }

            var result = new CommandResult
            {
                Success = await productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken),
                Result = product?.Id
            };

            return result;
        }
    }
}