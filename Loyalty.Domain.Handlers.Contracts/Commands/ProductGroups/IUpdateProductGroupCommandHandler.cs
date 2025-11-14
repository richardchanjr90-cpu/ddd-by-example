using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.ProductGroups;
using MediatR;

namespace Loyalty.Domain.Handlers.Contracts.Commands.ProductGroups
{
    public interface IUpdateProductGroupCommandHandler : IRequestHandler<UpdateProductGroupCommand, ICommandResult>
    {
    }
}