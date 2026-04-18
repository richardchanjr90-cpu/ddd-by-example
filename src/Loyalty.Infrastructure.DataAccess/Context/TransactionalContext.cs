using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Loyalty.Infrastructure.DataAccess.Context
{
    public abstract class TransactionalContext : DbContext
    {
        protected TransactionalContext(DbContextOptions<LoyaltyDbContext> options)
        : base(options)
        {
        }

        private IDbContextTransaction currentTransaction;

        public IDbContextTransaction GetCurrentTransaction() => currentTransaction;

        public bool HasActiveTransaction => currentTransaction != null;

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (currentTransaction != null)
            {
                return null;
            }

            currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            if (transaction != currentTransaction)
            {
                throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");
            }

            try
            {
                await SaveChangesAsync(default);
                await transaction.CommitAsync();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (currentTransaction != null)
                {
                    currentTransaction.Dispose();
                    currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                currentTransaction?.Rollback();
            }
            finally
            {
                if (currentTransaction != null)
                {
                    currentTransaction.Dispose();
                    currentTransaction = null;
                }
            }
        }
    }
}
