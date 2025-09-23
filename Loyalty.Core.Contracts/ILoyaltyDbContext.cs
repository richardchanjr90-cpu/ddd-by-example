using Loyalty.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Core.Contracts
{
    public interface ILoyaltyDbContext : IDbContext
    {
        DbSet<Location> Locations { get; set; }

        DbSet<LoyaltyProductGroup> LoyaltyProducts { get; set; }

        DbSet<LoyaltyProgram> LoyaltyPrograms { get; set; }

        DbSet<LoyaltyProgramRule> LoyaltyRules { get; set; }

        DbSet<Product> Products { get; set; }

        DbSet<ProductGroup> ProductGroups { get; set; }

        DbSet<Purchase> Purchases { get; set; }

        DbSet<VenueDetails> VenueDetails { get; set; }

        DbSet<Venue> Venues { get; set; }
    }
}
