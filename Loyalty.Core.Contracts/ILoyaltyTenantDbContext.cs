using System.Collections.Generic;
using Loyalty.Core.Entities.Base;

namespace Loyalty.Core.Contracts
{
    public interface ILoyaltyTenantDbContext : ILoyaltyDbContext
    {
        IEnumerable<Entity> GetModifiedOrAddedEntitiesBeforeSaveChanges();
    }
}