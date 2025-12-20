using Loyalty.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Core.Contracts
{
    public interface ILoyaltyTenantDbContext : ILoyaltyDbContext
    {
    }
}