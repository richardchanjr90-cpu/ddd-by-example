using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyPrograms;
using MediatR;

namespace Loyalty.Domain.Handlers.Contracts.Commands.LoyaltyPrograms
{
    public interface ICreateLoyaltyProgramCommandHandler : IRequestHandler<CreateLoyaltyProgramCommand, ICommandResult>
    {
    }
}
