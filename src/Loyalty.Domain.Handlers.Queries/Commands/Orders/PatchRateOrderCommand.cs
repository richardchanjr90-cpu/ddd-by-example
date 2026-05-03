using Loyalty.Shared.Contracts.Enums;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Queries.Commands.Orders
{
    public class PatchRateOrderCommand : IRequest<ICommandResult>
    {
        public long OrderId { get; set; }

        public string Comment { get; set; }

        public OrderVenueRate Rate { get; set; }
    }
}
