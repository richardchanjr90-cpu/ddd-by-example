using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Aggregates.LoyaltyPrograms;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyPrograms;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Infrastructure.Handlers.Commands.LoyaltyPrograms
{
    public class CreateLoyaltyProgramCommandHandler
        : IRequestHandler<CreateLoyaltyProgramCommand, ICommandResult>
    {
        private readonly ILoyaltyProgramRepository programRepository;

        public CreateLoyaltyProgramCommandHandler(ILoyaltyProgramRepository programRepository)
        {
            this.programRepository = programRepository;
        }

        public async Task<ICommandResult> Handle(
            CreateLoyaltyProgramCommand request,
            CancellationToken cancellationToken)
        {
            var program = new LoyaltyProgram(
                request.Name,
                request.Description,
                request.StartedDate,
                request.EndedDate,
                request.VenueId,
                request.Url);

            await programRepository.AddAsync(program);

            var result = new CommandResult
            {
                Success = await programRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken),
                Result = program.Id
            };

            return result;
        }
    }
}