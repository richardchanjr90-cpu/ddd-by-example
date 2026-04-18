using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Loyalty.Infrastructure.DataAccess.Context.Scoped
{
    public static class DbContextScopedExtensions
    {
        public static void AddScopedContext<TK, T>(this IServiceCollection services)
            where T : DbContext, TK
            where TK : class
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<ObjectStore>();
            services.AddTransient<T>();
            services.AddTransient<TK, T>((x) => ContextFactory<T>(x));
        }

        public static void AddScopedReplacement<T>(this IServiceCollection services)
            where T : class
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<ObjectStore>();
            services.AddTransient<T>((x) => ObjectFactory<T>(x));
        }

        public static void AddScopedReplacement<T>(this IServiceCollection services, T instance)
            where T : class
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<ObjectStore>();
            services.AddTransient<T>((x) => ObjectFactory<T>(x, instance));
        }

        private static T ObjectFactory<T>(IServiceProvider serviceProvider)
            where T : class
        {
            var accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            var store = serviceProvider.GetRequiredService<ObjectStore>();

            var hashCode = accessor.HttpContext.GetHashCode();
            var item = store.GetOrNull<T>(hashCode);

            if (item == null)
            {
                var createdContext = (T)serviceProvider.GetRequiredService(typeof(T));

                store.Save(hashCode, createdContext);
                item = createdContext;
            }

            return null;
        }

        private static T ObjectFactory<T>(IServiceProvider serviceProvider, T instance)
            where T : class
        {
            var accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            var store = serviceProvider.GetRequiredService<ObjectStore>();

            var hashCode = accessor.HttpContext.GetHashCode();
            var item = store.GetOrNull<T>(hashCode);

            if (instance != null)
            {
                store.Save(hashCode, instance);
            }
            else
            {
                instance = store.GetOrNull<T>(hashCode);
            }

            return instance;
        }

        private static T ContextFactory<T>(IServiceProvider serviceProvider)
            where T : DbContext
        {
            var accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            var store = serviceProvider.GetRequiredService<ObjectStore>();

            if (accessor.HttpContext != null)
            {
                var dbContext = new DbContextScoped<T>(accessor, store, serviceProvider);
                return dbContext.Context;
            }

            return null;
        }
    }
}
