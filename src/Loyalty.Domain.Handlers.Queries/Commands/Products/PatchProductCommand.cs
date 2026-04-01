using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Queries.Commands.Products
{
    public class PatchProductCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }

        public bool IsAvailableForOrder { get; set; }
    }
}