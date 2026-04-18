using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Aggregates.Products;
using Loyalty.Core.Entities.Aggregates.Purchases;
using Loyalty.Core.Entities.SeedWork.Interfaces;

namespace Loyalty.Core.Entities.Interfaces.Repository
{
    public interface IPurchaseRepository : IRepository<Purchase>
    {
        Task<Purchase> GetAsync(long id, CancellationToken token = default);

        Task<List<Purchase>> GetByVenueAsync(long id, CancellationToken token = default);

        Task<Purchase> AddAsync(Purchase group);

        Purchase Update(Purchase group);
    }
}
