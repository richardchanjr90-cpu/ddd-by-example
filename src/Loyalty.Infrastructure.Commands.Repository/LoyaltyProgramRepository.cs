using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Aggregates.LoyaltyPrograms;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Core.Entities.SeedWork.Interfaces;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Commands.Repository
{
    public class LoyaltyProgramRepository : ILoyaltyProgramRepository
    {
        public IUnitOfWork UnitOfWork => context;

        private readonly ILoyaltyTenantDbContext context;

        public LoyaltyProgramRepository(ILoyaltyTenantDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<LoyaltyProgram> GetAsync(long id, CancellationToken token = default)
        {
            var program = await context
                .LoyaltyPrograms
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync(token);

            return program;
        }

        public async Task<List<LoyaltyProgram>> GetByVenueAsync(long id, CancellationToken token = default)
        {
            var programs = await context
                .LoyaltyPrograms
                .Where(x => x.VenueId == id)
                .ToListAsync(token);

            return programs;
        }

        public async Task<LoyaltyProgram> GetByGroupId(long id, CancellationToken token = default)
        {
            var purchase = await context
                .LoyaltyPrograms
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync(token);

            return purchase;
        }

        public async Task<LoyaltyProgram> AddAsync(LoyaltyProgram program)
        {
            var result = program;

            if (program.IsTransient())
            {
                result = (await context.LoyaltyPrograms
                    .AddAsync(program)).Entity;
            }

            return result;
        }

        public LoyaltyProgram Update(LoyaltyProgram program)
        {
            return context.LoyaltyPrograms
                .Update(program).Entity;
        }
    }
}
