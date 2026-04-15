using System.Collections.Generic;
using Loyalty.Core.Entities.SeedWork;
using Loyalty.Core.Entities.SeedWork.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Loyalty.Infrastructure.DataAccess.Context.Interface
{
    public interface ILoyaltyTenantDbContext : ILoyaltyDbContext, IUnitOfWork
    {
        IEnumerable<Entity> GetModifiedOrAddedEntitiesBeforeSaveChanges();

        IDbContextTransaction GetCurrentTransaction();
    }
}