using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.ProductGroups;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Infrastructure.Handlers.Commands.Commands.ProductGroups
{
    public class ArchiveProductGroupCommandHandler
        : IRequestHandler<ArchiveProductGroupCommand, ICommandResult>
    {
        private readonly IProductGroupRepository groupRepository;
        private readonly ILoyaltyProgramRepository loyaltyProgramRepository;

        public ArchiveProductGroupCommandHandler(
            IProductGroupRepository groupRepository, 
            ILoyaltyProgramRepository loyaltyProgramRepository)
        {
            this.groupRepository = groupRepository;
            this.loyaltyProgramRepository = loyaltyProgramRepository;
        }

        public async Task<ICommandResult> Handle(
            ArchiveProductGroupCommand request,
            CancellationToken cancellationToken)
        {
            var loyaltyGroups = await loyaltyProgramRepository.GetByGroupId(request.Id, cancellationToken);

            if (loyaltyGroups != null && loyaltyGroups.Any())
            {
                throw new LoyaltyValidationException(
                    "Can't delete group if it is a member of active loyalty program.", 
                    ErrorCode.INCORRECT_PRODUCT_GROUP);
            }

            var group = await groupRepository
                .GetAsync(request.Id, cancellationToken);

            group?.Archive();

            groupRepository.Update(group);

            var result = new CommandResult
            {
                Success = await groupRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken),
                Result = group?.Id
            };

            return result;
        }
    }
}