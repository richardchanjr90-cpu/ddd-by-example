using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Domain.Contracts;
using Loyalty.Domain.Handlers.Notifications.Orders;
using Loyalty.Domain.Handlers.Queries.Commands.Orders;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Loyalty.Shared.Contracts.Enums;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Handlers.Commands.Orders
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
