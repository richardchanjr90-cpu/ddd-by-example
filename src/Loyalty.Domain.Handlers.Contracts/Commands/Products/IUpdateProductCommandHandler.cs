using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Products;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Contracts.Commands.Products
{
    public interface IUpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ICommandResult>
    {
    }
}