using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Aggregates.Venues;
using Loyalty.Core.Entities.SeedWork.Interfaces;

namespace Loyalty.Core.Entities.Interfaces.Repository
{
    public interface IVenueRepository : IRepository<Venue>
    {
        Task<Venue> GetAsync(long venueId, CancellationToken token = default);
    }
}
