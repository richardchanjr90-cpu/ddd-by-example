using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Aggregates.Products;
using Loyalty.Core.Entities.SeedWork.Interfaces;

namespace Loyalty.Core.Entities.Interfaces.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetAsync(long id, CancellationToken token = default);

        Task<List<Product>> GetByGroupAsync(long id, CancellationToken token = default);

        Task<List<Product>> GetByVenueAsync(long id, CancellationToken token = default);

        Task<Product> AddAsync(Product group);

        Product Update(Product group);
    }
}
