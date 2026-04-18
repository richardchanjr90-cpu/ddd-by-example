using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Aggregates.Orders;
using Loyalty.Core.Entities.SeedWork.Interfaces;

namespace Loyalty.Core.Entities.Interfaces.Repository
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> AddAsync(Order user);

        Order Update(Order rate);

        Task<Order> GetAsync(long orderId, string userId, CancellationToken token = default);
    }
}
