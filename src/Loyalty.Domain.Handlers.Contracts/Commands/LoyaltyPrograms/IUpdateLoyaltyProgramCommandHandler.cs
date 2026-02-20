using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyPrograms;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Contracts.Commands.LoyaltyPrograms
{
    public interface IUpdateLoyaltyProgramCommandHandler : IRequestHandler<UpdateLoyaltyProgramCommand, ICommandResult>
    {
    }
}