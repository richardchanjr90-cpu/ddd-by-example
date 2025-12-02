using Loyalty.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Core.Contracts
{
    public interface ILoyaltyTenantDbContext : ILoyaltyDbContext
    {
    }
}