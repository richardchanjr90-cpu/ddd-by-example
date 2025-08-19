using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Loyalty.Core.Shared.Enums;
using Loyalty.Data.Contracts;
using Loyalty.Data.DataAccess;
using Loyalty.Data.Entities;

namespace Loyalty.Data.Migrations
{
    public static class DbSeeding
    {
        public static void Seed()
        {
            using (var context = new LoyaltyApiContextFactory().CreateDbContext())
            {
                context.Database.EnsureCreated();

                var venue = context.Venues.FirstOrDefault(v => v.Name == "Venue1");
                if (venue == null)
                {
                    context.Venues.Add(new Venue()
                    {
                     //VenueCategoryType = VenueCategoryType.CoffeeShop,
                        Created = DateTime.Now,
                        CreatedBy = Guid.Parse("0ABE336D-021C-40B5-BA95-909DAEB7CA40"),
                        Location = new Location()
                        {
                            City = "Minsk",
                            Latitude = 90,
                            Longitude = 180
                        },
                        LogoUrl = "http://clipart-library.com/images/8cEb974Xi.jpg",
                        Description = "Venue without details and programs.",
                        Modified = DateTime.Now,
                        ModifiedBy = Guid.Parse("0ABE336D-021C-40B5-BA95-909DAEB7CA40"),
                        IsArchived = false,
                        IsPublished = false,
                        Name = "Venue1",
                        Type = VenueType.Single,
                        OwnerId = Guid.Parse("0ABE336D-021C-40B5-BA95-909DAEB7CA40"),
                    });
                }

                context.SaveChanges();
            }
        }
    }
}
