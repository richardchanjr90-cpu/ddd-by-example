using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Queries.Commands.ProductGroups
{
    public class PatchProductGroupCommand : IRequest<ICommandResult>
    {
        public long Id { get; set; }

        public bool IsAvailableForOrder { get; set; }
    }
}