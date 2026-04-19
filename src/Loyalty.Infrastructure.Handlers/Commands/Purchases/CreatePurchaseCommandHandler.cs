using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Entities.Aggregates.Products;
using Loyalty.Core.Entities.Aggregates.Purchases;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.Purchase;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Infrastructure.Handlers.Commands.Purchases
{
    public class CreatePurchaseCommandHandler : 
        IRequestHandler<CreatePurchaseCommand, ICommandResult>
    {
        private readonly IPurchaseRepository purchaseRepository;
        private readonly IProductRepository productRepository;
        private readonly ILoyaltyProgramRepository programRepository;

        public CreatePurchaseCommandHandler(
            IPurchaseRepository purchaseRepository, 
            IProductRepository productRepository,
            ILoyaltyProgramRepository programRepository)
        {
            this.purchaseRepository = purchaseRepository;
            this.productRepository = productRepository;
            this.programRepository = programRepository;
        }

        public async Task<ICommandResult> Handle(
            CreatePurchaseCommand request,
            CancellationToken cancellationToken)
        {
            Product product = null;
            if (request.ProductId.HasValue)
            {
                product = await productRepository.GetAsync(request.ProductId.Value, cancellationToken);

                if (product == null)
                {
                    throw new LoyaltyValidationException("Product does not belong to this venue or does not exist.", ErrorCode.INCORRECT_PRODUCT);
                }
            }

            var program = await programRepository.GetByGroupId(request.LoyaltyProductGroupId, cancellationToken);
            if (program == null)
            {
                throw new LoyaltyValidationException(
                    "LoyaltyProductGroup does not belong to this venue or does not exist.",
                    ErrorCode.INCORRECT_LOYALTY_GROUP);
            }

            var purchase = Purchase.Assign(
                request.WorkerId,
                request.LoyaltyProductGroupId, 
                request.VenueId, 
                product?.Id, 
                request.UserId, 
                request.Value);

            await purchaseRepository.AddAsync(purchase);

            var result = new CommandResult
            {
                Success = await purchaseRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken),
                Result = purchase.Id
            };

            return result;
        }
    }
}