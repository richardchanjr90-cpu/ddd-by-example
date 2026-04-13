using System.Collections.Generic;
using Loyalty.Core.Entities.SeedWork;
using Loyalty.Core.Entities.SeedWork.Interfaces;

namespace Loyalty.Infrastructure.DataAccess.Context.Interface
{
    public interface ILoyaltyTenantDbContext : ILoyaltyDbContext, IUnitOfWork
    {
        IEnumerable<Entity> GetModifiedOrAddedEntitiesBeforeSaveChanges();
    }
}