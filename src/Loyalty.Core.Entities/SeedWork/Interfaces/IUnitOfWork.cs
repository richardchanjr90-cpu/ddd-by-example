using System;
using System.Threading;
using System.Threading.Tasks;

namespace Loyalty.Core.Entities.SeedWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
