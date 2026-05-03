using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.Products;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Infrastructure.Handlers.Commands.Commands.Products
{
    public class UpdateProductCommandHandler
        : IRequestHandler<UpdateProductCommand, ICommandResult>
    {
        private readonly IProductRepository productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<ICommandResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetAsync(request.Id, cancellationToken);

            product?.UpdateProduct(
                request.Name, 
                request.Icon, 
                request.Description,
                request.Price, 
                request.ExternalUid);

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