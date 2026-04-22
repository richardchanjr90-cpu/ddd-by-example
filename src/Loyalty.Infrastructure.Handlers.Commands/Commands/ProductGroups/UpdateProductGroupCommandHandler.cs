using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.ProductGroups;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Infrastructure.Handlers.Commands.Commands.ProductGroups
{
    public class UpdateProductGroupCommandHandler 
        : IRequestHandler<UpdateProductGroupCommand, ICommandResult>
    {
        private readonly IProductGroupRepository groupRepository;

        public UpdateProductGroupCommandHandler(IProductGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public async Task<ICommandResult> Handle(UpdateProductGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await groupRepository
                .GetAsync(request.Id, cancellationToken);

            group.Update(request.Name, request.Icon);

            groupRepository.Update(group);

            var result = new CommandResult
            {
                Success = await groupRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken),
                Result = group.Id
            };

            return result;
        }
    }
}