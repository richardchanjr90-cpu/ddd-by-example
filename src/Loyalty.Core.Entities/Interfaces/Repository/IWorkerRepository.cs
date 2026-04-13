using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Aggregates.Workers;
using Loyalty.Core.Entities.SeedWork.Interfaces;

namespace Loyalty.Core.Entities.Interfaces.Repository
{
    public interface IWorkerRepository : IRepository<Worker>
    {
        Task<Worker> GetAsync(long id, CancellationToken token = default);

        Task<Worker> GetByUidAsync(string workerId, CancellationToken token = default);

        Task<Worker> GetByPhoneAsync(string phone, CancellationToken token = default);

        Task<Worker> AddAsync(Worker venue);

        public Worker Update(Worker venue);
    }
}
