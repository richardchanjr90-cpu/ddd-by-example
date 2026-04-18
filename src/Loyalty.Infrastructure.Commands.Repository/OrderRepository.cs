using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Aggregates.Orders;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Core.Entities.SeedWork.Interfaces;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Infrastructure.DataAccess.Context;
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

        public async Task<Order> AddAsync(Order rate)
        {
            var result = rate;

            if (rate.IsTransient())
            {
                result = (await context.Orders
                    .AddAsync(rate)).Entity;
            }

            return result;
        }

        public Order Update(Order rate)
        {
            return context.Orders
                .Update(rate).Entity;
        }

        public async Task<Order> GetAsync(long orderId, string userId, CancellationToken token = default)
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
