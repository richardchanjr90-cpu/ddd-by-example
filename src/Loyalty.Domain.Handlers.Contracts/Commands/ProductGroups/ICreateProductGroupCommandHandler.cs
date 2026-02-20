using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.ProductGroups;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Contracts.Commands.ProductGroups
{
    public interface ICreateProductGroupCommandHandler : IRequestHandler<CreateProductGroupCommand, ICommandResult>
    {
    }
}