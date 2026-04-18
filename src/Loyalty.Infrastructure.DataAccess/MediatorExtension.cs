using System.Linq;
using System.Threading.Tasks;
using Loyalty.Core.Entities.SeedWork;
using Loyalty.Infrastructure.DataAccess.Context;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Loyalty.Infrastructure.DataAccess
{
    internal static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, LoyaltyDbContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var entityEntries = domainEntities as EntityEntry<Entity>[] ?? domainEntities.ToArray();
            var domainEvents = entityEntries
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            entityEntries.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }
        }
    }
}
