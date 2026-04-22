using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyPrograms;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Infrastructure.Handlers.Commands.Commands.LoyaltyPrograms
{
    public class UpdateLoyaltyProgramCommandHandler
        : IRequestHandler<UpdateLoyaltyProgramCommand, ICommandResult>
    {
        private readonly ILoyaltyProgramRepository programRepository;

        public UpdateLoyaltyProgramCommandHandler(ILoyaltyProgramRepository programRepository)
        {
            this.programRepository = programRepository;
        }

        public async Task<ICommandResult> Handle(
            UpdateLoyaltyProgramCommand request,
            CancellationToken cancellationToken)
        {
            var program = await programRepository.GetAsync(request.Id, cancellationToken);

            if (program == null)
            {
                throw new LoyaltyValidationException("Does not exist.", ErrorCode.INCORRECT_LOYALTY_PROGRAM);
            }

            program.Update(
                request.Name, 
                request.Description, 
                request.StartedDate, 
                request.EndedDate, 
                request.IsPublished,
                request.ExternalProgramUri);

            programRepository.Update(program);

            var result = new CommandResult
            {
                Success = await programRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken),
                Result = program.Id
            };

            return result;
        }
    }
}