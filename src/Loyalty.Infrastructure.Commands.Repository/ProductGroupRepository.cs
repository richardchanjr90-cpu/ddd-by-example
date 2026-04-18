using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Aggregates.ProductGroups;
using Loyalty.Core.Entities.Aggregates.Products;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Core.Entities.SeedWork.Interfaces;
using Loyalty.Infrastructure.DataAccess.Context;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Commands.Repository
{
    public class ProductGroupRepository : IProductGroupRepository
    {
        public IUnitOfWork UnitOfWork => context;

        private readonly ILoyaltyTenantDbContext context;

        public ProductGroupRepository(ILoyaltyTenantDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ProductGroup> AddAsync(ProductGroup group)
        {
            var result = group;

            if (group.IsTransient())
            {
                result = (await context.ProductGroups
                    .AddAsync(group)).Entity;
            }

            return result;
        }

        public ProductGroup Update(ProductGroup group)
        {
            return context.ProductGroups
                .Update(group).Entity;
        }

        public async Task<ProductGroup> GetAsync(long id, CancellationToken token = default)
        {
            var group = await context
                .ProductGroups
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync(token);

            if (group == null)
            {
                group = context
                    .ProductGroups
                    .Local
                    .SingleOrDefault(x => x.Id == id);
            }

            return group;
        }
    }
}
