using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Aggregates.Products;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Core.Entities.SeedWork.Interfaces;
using Loyalty.Infrastructure.DataAccess;
using Loyalty.Infrastructure.DataAccess.Context;
using Loyalty.Infrastructure.DataAccess.Context.Interface;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.Commands.Repository
{
    public class ProductRepository : IProductRepository
    {
        public IUnitOfWork UnitOfWork => context;

        private readonly ILoyaltyTenantDbContext context;

        public ProductRepository(ILoyaltyTenantDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Product> GetAsync(long id, CancellationToken token = default)
        {
            var product = await context
                .Products
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync(token);

            if (product == null)
            {
                product = context
                    .Products
                    .Local
                    .SingleOrDefault(x => x.Id == id);
            }

            return product;
        }

        public async Task<List<Product>> GetByGroupAsync(long id, CancellationToken token = default)
        {
            var products = await context
                .Products
                .Where(x => x.ProductGroupId == id)
                .ToListAsync(token);

            if (products == null)
            {
                products = context
                    .Products
                    .Local
                    .Where(x => x.Id == id)
                    .ToList();
            }

            return products;
        }

        public async Task<List<Product>> GetByVenueAsync(long id, CancellationToken token = default)
        {
            var products = await context
                .Products
                .Where(x => x.VenueId == id)
                .ToListAsync(token);

            if (products == null)
            {
                products = context
                    .Products
                    .Local
                    .Where(x => x.VenueId == id)
                    .ToList();
            }

            return products;
        }

        public async Task<Product> AddAsync(Product product)
        {
            var result = product;

            if (product.IsTransient())
            {
                result = (await context.Products
                    .AddAsync(product)).Entity;
            }

            return result;
        }

        public Product Update(Product product)
        {
            return context.Products
                .Update(product).Entity;
        }
    }
}
