using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.ProductGroups;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Infrastructure.Handlers.Commands.ProductGroups
{
    public class PatchProductGroupCommandHandler
        : IRequestHandler<PatchProductGroupCommand, ICommandResult>
    {
        private readonly IProductGroupRepository groupRepository;

        public PatchProductGroupCommandHandler(IProductGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public async Task<ICommandResult> Handle(PatchProductGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await groupRepository
                .GetAsync(request.Id, cancellationToken);

            if (group != null)
            {
                if (request.IsAvailableForOrder)
                {
                    group.ShowToCustomer();
                }
                else
                {
                    group.HideFromCustomer();
                }
                groupRepository.Update(group);
            }

            var result = new CommandResult
            {
                Success = await groupRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken),
                Result = group?.Id
            };

            return result;
        }
    }
}