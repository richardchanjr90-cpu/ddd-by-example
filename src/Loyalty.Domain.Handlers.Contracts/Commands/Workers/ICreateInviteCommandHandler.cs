using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Workers;
using Loyalty.Domain.Handlers.Queries.Commands.Workers.Invites;
using MediatR;

namespace Loyalty.Domain.Handlers.Contracts.Commands.Workers
{
    public interface ICreateInviteCommandHandler : IRequestHandler<CreateInviteCommand, ICommandResult>
    {
    }
}