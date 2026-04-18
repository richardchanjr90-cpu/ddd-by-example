using System.Collections.Generic;
using System.Threading.Tasks;
using Loyalty.Core.Entities.SeedWork;
using Microsoft.EntityFrameworkCore.Storage;

namespace Loyalty.Infrastructure.DataAccess.Context.Interface
{
    public interface ILoyaltyTenantDbContext : ILoyaltyDbContext
    {
        IEnumerable<Entity> GetModifiedOrAddedEntitiesBeforeSaveChanges();

        IDbContextTransaction GetCurrentTransaction();

        bool HasActiveTransaction { get; }

        Task CommitTransactionAsync(IDbContextTransaction transaction);

        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}