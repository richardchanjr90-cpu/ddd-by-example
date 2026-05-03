using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Aggregates.LoyaltyPrograms;
using Loyalty.Core.Entities.SeedWork.Interfaces;

namespace Loyalty.Core.Entities.Interfaces.Repository
{
    public interface ILoyaltyProgramRepository : IRepository<LoyaltyProgram>
    {
        Task<LoyaltyProgram> GetAsync(long id, CancellationToken token = default);

        Task<List<LoyaltyProgram>> GetByVenueAsync(long id, CancellationToken token = default);

        Task<LoyaltyProgram> GetByLoyaltyGroupId(long id, CancellationToken token = default);

        Task<List<LoyaltyProductGroup>> GetByGroupId(long id, CancellationToken token = default);

        Task<LoyaltyProgram> AddAsync(LoyaltyProgram group);

        LoyaltyProgram Update(LoyaltyProgram group);
    }
}
