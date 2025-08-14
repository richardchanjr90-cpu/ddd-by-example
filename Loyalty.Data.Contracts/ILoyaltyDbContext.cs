using System;
using Loyalty.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Data.Contracts
{
    public interface ILoyaltyDbContext
    {
        DbSet<Card> Cards { get; set; }

        DbSet<GeoPosition> GeoPositions { get; set; }

        DbSet<LoyaltyProduct> LoyaltyProducts { get; set; }

        DbSet<LoyaltyProgram> LoyaltyPrograms { get; set; }

        DbSet<Stamp> Stamps { get; set; }

        DbSet<Venue> Venues { get; set; }
    }
}
