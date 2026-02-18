using System.Collections.Generic;
using Loyalty.Core.Entities.Base;

namespace Loyalty.Infrastructure.DataAccess
{
    public interface ILoyaltyTenantDbContext : ILoyaltyDbContext
    {
        IEnumerable<Entity> GetModifiedOrAddedEntitiesBeforeSaveChanges();
    }
}