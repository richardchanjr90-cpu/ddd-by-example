using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Aggregates.Products;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.Products;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Infrastructure.Handlers.Commands.Commands.Products
{
    public class CreateProductCommandHandler
        : IRequestHandler<CreateProductCommand, ICommandResult>
    {
        private readonly IProductGroupRepository productGroupRepository;
        private readonly IProductRepository productRepository;

        public CreateProductCommandHandler(
            IProductGroupRepository productGroupRepository, 
            IProductRepository productRepository)
        {
            this.productGroupRepository = productGroupRepository;
            this.productRepository = productRepository;
        }

        public async Task<ICommandResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var group = await productGroupRepository
                .GetAsync(request.ProductGroupId, cancellationToken);

            var product = new Product(
                request.Name, 
                request.Icon, 
                request.Price,
                request.Description,
                group);

            await productRepository.AddAsync(product);

            var result = new CommandResult
            {
                Success = await productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken),
                Result = product?.Id
            };

            return result;
        }
    }
}
