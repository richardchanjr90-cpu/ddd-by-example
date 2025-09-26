using System.Threading;
using System.Threading.Tasks;

namespace Loyalty.Core.Contracts
{
    public interface IDbContext
    {
        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
