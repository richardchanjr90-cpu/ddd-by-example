using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Aggregates.Orders;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Core.Entities.SeedWork.Interfaces;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Commands.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public IUnitOfWork UnitOfWork => context;

        private readonly ILoyaltyTenantDbContext context;

        public OrderRepository(ILoyaltyTenantDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Order Update(Order rate)
        {
            return context.Orders
                .Update(rate).Entity;
        }

        public async Task<Order> GetAsync(long orderId, CancellationToken token = default)
        {
            var order = await context
                .Orders
                .Where(x => x.Id == orderId)
                .SingleOrDefaultAsync(token);

            if (order == null)
            {
                order = context
                    .Orders
                    .Local
                    .SingleOrDefault(x => x.Id == orderId);
            }

            return order;
        }
    }
}
