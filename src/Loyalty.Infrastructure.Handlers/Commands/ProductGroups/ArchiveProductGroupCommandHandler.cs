using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.ProductGroups;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Infrastructure.Handlers.Commands.ProductGroups
{
    public class ArchiveProductGroupCommandHandler
        : IRequestHandler<ArchiveProductGroupCommand, ICommandResult>
    {
        private readonly IProductGroupRepository groupRepository;

        public ArchiveProductGroupCommandHandler(IProductGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public async Task<ICommandResult> Handle(
            ArchiveProductGroupCommand request,
            CancellationToken cancellationToken)
        {
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