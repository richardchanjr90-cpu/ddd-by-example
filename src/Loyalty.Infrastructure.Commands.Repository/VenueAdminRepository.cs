using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Aggregates.Venues;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Core.Entities.SeedWork.Interfaces;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Commands.Repository
{
    public class VenueAdminRepository : IVenueAdminRepository
    {
        public IUnitOfWork UnitOfWork => context;

        private readonly ILoyaltyDbContext context;

        public VenueAdminRepository(ILoyaltyDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Venue> GetWithoutQueryFiltersAsync(long venueId, CancellationToken token = default)
        {
            var venue = await context
                .Venues
                .IgnoreQueryFilters()
                .Where(x => x.Id == venueId)
                .SingleOrDefaultAsync(token);

            return venue;
        }

        public Venue Update(Venue venue)
        {
            return context.Venues
                .Update(venue).Entity;
        }
    }
}
