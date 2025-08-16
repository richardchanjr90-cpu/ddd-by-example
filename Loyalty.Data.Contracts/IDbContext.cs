using System;
using System.Threading;
using System.Threading.Tasks;

namespace Loyalty.Data.Contracts
{
    public interface IDbContext
    {
        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
