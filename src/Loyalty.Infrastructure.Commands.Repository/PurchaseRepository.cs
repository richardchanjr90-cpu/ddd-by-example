using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Aggregates.Purchases;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Core.Entities.SeedWork.Interfaces;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Commands.Repository
{
    public class PurchaseRepository: IPurchaseRepository
    {
        public IUnitOfWork UnitOfWork => context;

        private readonly ILoyaltyTenantDbContext context;

        public PurchaseRepository(ILoyaltyTenantDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Purchase> GetAsync(long id, CancellationToken token = default)
        {
            var purchase = await context
                .Purchases
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync(token);

            return purchase;
        }

        public async Task<Purchase> AddAsync(Purchase purchase)
        {
            var result = purchase;

            if (purchase.IsTransient())
            {
                result = (await context.Purchases
                    .AddAsync(purchase)).Entity;
            }

            return result;
        }

        public Purchase Update(Purchase purchase)
        {
            return context.Purchases
                .Update(purchase).Entity;
        }

        public async Task<List<Purchase>> GetPurchasesForUserAsync(string userId, CancellationToken cancellationToken)
        {
            var purchases = await context
                .Purchases
                .Where(x => x.UserId == userId)
                .ToListAsync(cancellationToken);

            return purchases;
        }
    }
}
