using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Aggregates.Orders;
using Loyalty.Core.Entities.SeedWork.Interfaces;

namespace Loyalty.Core.Entities.Interfaces.Repository
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order Update(Order rate);

        Task<Order> GetAsync(long orderId, CancellationToken token = default);
    }
}
