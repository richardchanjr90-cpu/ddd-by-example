using Loyalty.Core.Entities;
using Loyalty.Core.UserEntities;
using Microsoft.EntityFrameworkCore;

namespace Loyalty.Core.Contracts
{
    public interface IUserDbContext : IDbContext
    {
        DbSet<UserCode> UserCodes { get; set; }

        DbSet<User> Users { get; set; }

        DbSet<UserPoint> UserPoints { get; set; }
    }
}