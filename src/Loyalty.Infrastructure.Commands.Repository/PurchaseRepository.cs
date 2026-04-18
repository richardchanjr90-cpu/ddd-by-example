using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Core.Entities.Aggregates.Purchases;
using Loyalty.Core.Entities.Interfaces.Repository;
using Loyalty.Core.Entities.SeedWork.Interfaces;

namespace Loyalty.Infrastructure.Commands.Repository
{
    public class PurchaseRepository: IPurchaseRepository
    {
        public IUnitOfWork UnitOfWork { get; }

        public Task<Purchase> GetAsync(long id, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<Purchase>> GetByVenueAsync(long id, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<Purchase> AddAsync(Purchase @group)
        {
            throw new NotImplementedException();
        }

        public Purchase Update(Purchase @group)
        {
            throw new NotImplementedException();
        }
    }
}
