using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Aggregates.ProductGroups;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.ProductGroups;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Infrastructure.Handlers.Commands.ProductGroups
{
    public class CreateProductGroupCommandHandler
        : IRequestHandler<CreateProductGroupCommand, ICommandResult>
    {
        private readonly IProductGroupRepository groupRepository;

        public CreateProductGroupCommandHandler(IProductGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public async Task<ICommandResult> Handle(CreateProductGroupCommand request, CancellationToken cancellationToken)
        {
            var group = new ProductGroup(
                request.VenueId,
                request.Name,
                request.Icon);

            await groupRepository.AddAsync(group);

            return new CommandResult
            {
                Success = await groupRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken),
                Result = group.Id
            };
        }
    }
}