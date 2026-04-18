using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Aggregates.LoyaltyPrograms;
using Loyalty.Core.Entities.Aggregates.Purchases;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Core.Entities.SeedWork.Interfaces;
using Loyalty.Infrastructure.DataAccess.Context.Interface;

namespace Loyalty.Infrastructure.Commands.Repository
{
    public class LoyaltyProgramRepository : ILoyaltyProgramRepository
    {
        public IUnitOfWork UnitOfWork => context;

        private readonly ILoyaltyTenantDbContext context;

        public Task<LoyaltyProgram> GetAsync(long id, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<LoyaltyProgram>> GetByVenueAsync(long id, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<LoyaltyProgram> AddAsync(LoyaltyProgram @group)
        {
            throw new NotImplementedException();
        }

        public LoyaltyProgram Update(Purchase @group)
        {
            throw new NotImplementedException();
        }
    }
}
