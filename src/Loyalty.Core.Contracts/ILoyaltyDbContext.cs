using Loyalty.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Core.Contracts
{
    public interface ILoyaltyDbContext : IDbContext
    {
        DbSet<LoyaltyProductGroup> LoyaltyProductGroups { get; set; }

        DbSet<LoyaltyProgram> LoyaltyPrograms { get; set; }

        DbSet<LoyaltyGroupRule> LoyaltyRules { get; set; }

        DbSet<Product> Products { get; set; }

        DbSet<ProductGroup> ProductGroups { get; set; }

        DbSet<Purchase> Purchases { get; set; }

        DbSet<Venue> Venues { get; set; }

        DbSet<VenueWorker> VenueWorkers { get; set; }

        DbSet<Worker> Workers { get; set; }

        DbSet<UserCode> UserCodes { get; set; }
    }
}