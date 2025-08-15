using Loyalty.Data.Contracts;
using Loyalty.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Data.DataAccess
{
    public class LoyaltyDbContext : DbContext, ILoyaltyDbContext
    {
        public DbSet<Card> Cards { get; set; }

        public DbSet<GeoPosition> GeoPositions { get; set; }

        public DbSet<LoyaltyProduct> LoyaltyProducts { get; set; }

        public DbSet<LoyaltyProgram> LoyaltyPrograms { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<Venue> Venues { get; set; }

        public DbSet<VenueDetails> VenueDetails { get; set; }

        public LoyaltyDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
