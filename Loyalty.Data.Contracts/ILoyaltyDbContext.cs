using System;
using Loyalty.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Data.Contracts
{
    public interface ILoyaltyDbContext : IDbContext
    {
        DbSet<Card> Cards { get; set; }

        DbSet<Location> Locations { get; set; }

        DbSet<LoyaltyProduct> LoyaltyProducts { get; set; }

        DbSet<LoyaltyProductGroup> LoyaltyProductGroups { get; set; }

        DbSet<LoyaltyProgram> LoyaltyPrograms { get; set; }

        DbSet<Purchase> Purchases { get; set; }

        DbSet<VenueDetails> VenueDetails { get; set; }

        DbSet<Venue> Venues { get; set; }

        DbSet<VenueCategory> VenueCategories { get; set; }
    }
}
