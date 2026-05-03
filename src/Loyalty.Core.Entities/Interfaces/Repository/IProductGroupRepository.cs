using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Aggregates.ProductGroups;
using Loyalty.Core.Entities.SeedWork.Interfaces;

namespace Loyalty.Core.Entities.Interfaces.Repository
{
    public interface IProductGroupRepository : IRepository<ProductGroup>
    {
        Task<ProductGroup> GetAsync(long id, CancellationToken token = default);

        Task<ProductGroup> AddAsync(ProductGroup group);

        ProductGroup Update(ProductGroup group);
    }
}
