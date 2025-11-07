using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.LoyaltyProductGroup;
using MediatR;

namespace Loyalty.Domain.Handlers.Contracts.Commands.LoyaltyProductGroups
{
    public interface IArchiveLoyaltyProductGroupCommandHandler
        : IRequestHandler<ArchiveLoyaltyProductGroupCommand, ICommandResult>
    {
    }
}