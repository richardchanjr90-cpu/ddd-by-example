using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Aggregates.Workers;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Core.Entities.SeedWork.Interfaces;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Commands.Repository
{
    public class WorkerRepository : IWorkerRepository
    {
        public IUnitOfWork UnitOfWork => context;

        private readonly ILoyaltyTenantDbContext context;

        public WorkerRepository(ILoyaltyTenantDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Worker> GetByUidAsync(string workerId, CancellationToken token = default)
        {
            var worker = await context
                .Workers
                .Where(x => x.WorkerId == workerId)
                .SingleOrDefaultAsync(token);

            return worker;
        }

        public async Task<Worker> GetByPhoneAsync(string phone, CancellationToken token = default)
        {
            var worker = await context
                .Workers
                .Where(x => x.Phone == phone)
                .SingleOrDefaultAsync(token);

            return worker;
        }

        public async Task<Worker> GetByEmailAsync(string email, CancellationToken token = default)
        {
            var worker = await context
                .Workers
                .Where(x => x.Email == email)
                .SingleOrDefaultAsync(token);

            return worker;
        }

        public async Task<Worker> GetAsync(long id, CancellationToken token = default)
        {
            var worker = await context
                .Workers
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync(token);

            return worker;
        }

        public async Task<Worker> AddAsync(Worker worker)
        {
            var result = worker;

            if (worker.IsTransient())
            {
                result = (await context.Workers
                    .AddAsync(worker)).Entity;
            }

            return result;
        }

        public Worker Update(Worker worker)
        {
            return context.Workers
                .Update(worker).Entity;
        }
    }
}
