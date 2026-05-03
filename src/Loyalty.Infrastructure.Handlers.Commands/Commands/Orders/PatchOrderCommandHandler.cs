using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Queries.Commands.Orders;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Infrastructure.Handlers.Commands.Commands.Orders
{
    public class PatchOrderCommandHandler
        : IRequestHandler<PatchOrderCommand, ICommandResult>
    {
        private readonly IOrderRepository orderRepository;

        public PatchOrderCommandHandler(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<ICommandResult> Handle(PatchOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await orderRepository.GetAsync(request.OrderId, cancellationToken);

            order?.UpdateStatus(request.Status, request.VenueComment);
            orderRepository.Update(order);

            var result = new CommandResult
            {
                Success = await orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken),
                Result = order?.Id
            };

            return result;
        }
    }
}
