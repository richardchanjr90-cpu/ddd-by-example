using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Domain.Handlers.Queries.Commands.Products;
using MediatR;

namespace Loyalty.Domain.Handlers.Contracts.Commands.Products
{
    public interface IArchiveProductCommandHandler : IRequestHandler<ArchiveProductCommand, ICommandResult>
    {
    }
}