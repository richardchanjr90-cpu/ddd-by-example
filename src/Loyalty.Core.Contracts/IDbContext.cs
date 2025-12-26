using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Loyalty.Core.Contracts
{
    public interface IDbContext
    {
        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity)
            where TEntity : class;
    }
}