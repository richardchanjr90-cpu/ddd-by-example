using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Loyalty.Infrastructure.DataAccess.Context.Scoped
{
    public class DbContextScoped<T> : IDisposable 
        where T : DbContext
    {
        private readonly int hashCode;

        public T Context { get; }

        public DbContextScoped(IHttpContextAccessor accessor, ObjectStore store, IServiceProvider provider)
        {
            hashCode = accessor.HttpContext.GetHashCode();
            var dbContext = store.GetOrNull<T>(hashCode);

            if (dbContext == null)
            {
                var createdContext = (T)provider.GetRequiredService(typeof(T));

                store.Save(hashCode, createdContext);
                dbContext = createdContext;
            }

            Context = dbContext;
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
