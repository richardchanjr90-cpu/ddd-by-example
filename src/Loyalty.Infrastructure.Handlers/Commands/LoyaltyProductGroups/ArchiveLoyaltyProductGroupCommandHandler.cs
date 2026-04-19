using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyProductGroup;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Infrastructure.Handlers.Commands.LoyaltyProductGroups
{
    public class ArchiveLoyaltyProductGroupCommandHandler
        : IRequestHandler<ArchiveLoyaltyProductGroupCommand, ICommandResult>
    {
        private readonly ILoyaltyProgramRepository programRepository;

        public ArchiveLoyaltyProductGroupCommandHandler(ILoyaltyProgramRepository programRepository)
        {
            this.programRepository = programRepository;
        }

        public async Task<ICommandResult> Handle(ArchiveLoyaltyProductGroupCommand request,
            CancellationToken cancellationToken)
        {
            var program = await programRepository.GetByGroupId(request.Id, cancellationToken);

            program.ArchiveGroup(request.Id);
            programRepository.Update(program);

            var result = new CommandResult
            {
                Success = await programRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken),
                Result = request.Id
            };

            return result;
        }
    }
}