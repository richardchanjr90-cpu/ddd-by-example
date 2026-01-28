using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Loyalty.Common.Shared.Constants;
using Loyalty.Common.Shared.Exceptions;
using Loyalty.Core.Contracts;
using Loyalty.Core.UserEntities;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Infrastructure.UserDataAccess
{
    public class UserDbContext : DbContext, IUserDbContext
    {
        public UserDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<UserCode> UserCodes { get; set; } 

        public DbSet<User> Users { get; set; } 

        public DbSet<UserPoint> UserPoints { get; set; }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
            {
                throw new LoyaltyValidationException("Duplicated entity", ex, ErrorCode.DUPLICATED_ENTITY);
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
            {
                throw new LoyaltyValidationException("Duplicated entity", ex, ErrorCode.DUPLICATED_ENTITY);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserCode>()
                .HasIndex(p => new { p.CodeValue })
                .IsUnique();
        }
    }
}
