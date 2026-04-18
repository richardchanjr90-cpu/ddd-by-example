using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Aggregates.Venues;
using Loyalty.Core.Entities.SeedWork.Interfaces;

namespace Loyalty.Core.Entities.Interfaces.Repository
{
    public interface IVenueAdminRepository : IRepository<Venue>
    {
        Task<Venue> GetWithoutQueryFiltersAsync(long venueId, CancellationToken token = default);

        public Venue Update(Venue venue);
    }
}
