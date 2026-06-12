# Multi-Tenancy in EF Core: One Database, Many Tenants, Zero Leaks

> ### Row-level multi-tenancy done right: global query filters for reads, and a write-side guard that makes cross-tenant leaks impossible.

*Part 5 of 5 in “Domain-Driven Design by Example” · ~11 min read*

**Suggested Medium tags:** Dotnet, Entity Framework, Multi Tenancy, Software Architecture, CSharp

> 🧩 Part of an open-source series — every code link points to the real file in the [ddd-by-example](https://github.com/richardchanjr90-cpu/ddd-by-example) repository, a production loyalty-program backend (not a toy example). Diagrams are embedded as images so they render directly here.

---
Our loyalty platform is a SaaS: hundreds of venues share one backend and one database. Venue A must never
see, edit, or even *accidentally count* venue B's orders, programs, or customers. Get this wrong once —
one query missing a `WHERE VenueId = @me` — and you don't get a bug report, you get a **data breach**.

The naive defence is to remember to add the tenant filter to every query and every save. That's a defence
that fails the first time a tired developer writes `_db.Orders.Where(o => o.Status == Ready)` and forgets
the rest. Security that depends on remembering is not security. This finale shows how the
[ddd-by-example](https://github.com/richardchanjr90-cpu/ddd-by-example) codebase makes tenant isolation
**the default** — automatic on reads, enforced on writes — so forgetting becomes impossible.

## Choosing a multi-tenancy model

There are three common shapes, in rising order of isolation and cost:

1. **Database-per-tenant** — strongest isolation, most operational overhead (hundreds of databases to
   migrate and back up).
2. **Schema-per-tenant** — one database, a schema each. Middle ground.
3. **Shared table, row-level** — every tenant's rows live in the same tables, partitioned by a tenant
   key column. Cheapest and simplest to operate; the burden shifts to *never leaking rows across the
   key*.

This backend uses **row-level**, keyed on `VenueId`. That choice is what makes the rest of this article
necessary: with shared tables, isolation is entirely your code's responsibility, so we make the code
enforce it structurally.

## Step 1: every tenant-scoped entity declares its tenant

From [Part 1](01-rich-domain-model.md), recall that aggregates inherit a small base. Tenant-scoped ones
inherit [`TenantEntity`](https://github.com/richardchanjr90-cpu/ddd-by-example/blob/main/src/Loyalty.Core.Entities/Base/TenantEntity.cs), which forces each to
declare *what its tenant is*:

```csharp
public abstract class TenantEntity : AuditableEntity, ITenantEntity
{
    [NotMapped]
    public abstract long TenantId { get; }
}
```

`[NotMapped]` because the tenant isn't a stored column on most aggregates — it's *derived*. An `Order`'s
tenant is its `VenueId`; a `LoyaltyProductGroup`'s tenant is its program's venue:

```csharp
public class Order : TenantEntity, IAggregateRoot
{
    public long VenueId { get; private set; }
    public override long TenantId => VenueId;     // this order belongs to this venue
}
```

Making `TenantId` abstract means a new aggregate *cannot compile* without answering "who owns you?" — the
type system nudges you toward isolation.

## Step 2: where does "the current tenant" come from?

The tenant is a property of the **request**, not the domain — so the domain stays ignorant of it, behind
an abstraction, [`ITenantProvider`](https://github.com/richardchanjr90-cpu/ddd-by-example/blob/main/src/Loyalty.Core.Contracts/ITenantProvider.cs):

```csharp
public interface ITenantProvider
{
    List<long> GetTenants();          // the venue ids this caller may touch
    ClaimsPrincipal Principal { get; }
}
```

The implementation reads the venues off the authenticated user's JWT claims
([`TenantTokenProvider`](https://github.com/richardchanjr90-cpu/ddd-by-example/blob/main/src/Loyalty.Infrastructure.DataAccess/TenantTokenProvider.cs)):

```csharp
public class TenantTokenProvider : ITenantProvider
{
    private readonly IHttpContextAccessor accessor;
    public ClaimsPrincipal Principal { get; }

    public TenantTokenProvider(IHttpContextAccessor accessor)
    {
        this.accessor = accessor;
        Principal = accessor.HttpContext.User;
    }

    public List<long> GetTenants() =>
        accessor.HttpContext.User.GetVenues()
            .Select(x => x.Trim('"'))
            .Select(long.Parse)
            .ToList();
}
```

Note it returns a *list* — a venue owner who manages several locations is several tenants at once. The
allowed set comes from a signed token, so a caller can't claim a venue they don't own.

## Step 3: reads are isolated automatically (global query filters)

![Tenant isolation — global query filters for reads, a write-side guard for writes](https://mermaid.ink/img/Zmxvd2NoYXJ0IFRCCiAgUkVRWyJIVFRQIHJlcXVlc3Q8YnIvPkpXVCB3aXRoIHZlbnVlIGNsYWltcyJdIC0tPiBUUFsiVGVuYW50VG9rZW5Qcm92aWRlcjxici8-R2V0VGVuYW50cygpIl0KICBUUCAtLT4gSURTWyJhbGxvd2VkIHZlbnVlIGlkcyJdCiAgSURTIC0tPiBRRlsiRUYgQ29yZSBnbG9iYWwgcXVlcnkgZmlsdGVyczxici8-cmVhZHMgYXV0by1zY29wZWQgcGVyIHRlbmFudCJdCiAgSURTIC0tPiBXR1siQWRkQXVkaXRJbmZvIHdyaXRlIGd1YXJkPGJyLz50aHJvdyBpZiBlbnRpdHkgVGVuYW50SWQgbm90IGFsbG93ZWQiXQogIFFGIC0tPiBEQlsoIlNRTCBTZXJ2ZXIiKV0KICBXRyAtLT4gREIK?type=png)

*Tenant isolation — global query filters for reads, a write-side guard for writes.*


This is the lever. EF Core's **global query filters** attach a predicate to *every* query against an
entity type — so the tenant `WHERE` clause is appended whether or not the developer remembers it. The
tenant-aware context ([`LoyaltyTenantDbContext`](https://github.com/richardchanjr90-cpu/ddd-by-example/blob/main/src/Loyalty.Infrastructure.DataAccess/Context/LoyaltyTenantDbContext.cs))
wires one per aggregate:

```csharp
internal List<long> TenantIds => provider.GetTenants();

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Venue>()           .HasQueryFilter(e => TenantIds.Contains(e.Id) && !e.IsArchived);
    modelBuilder.Entity<LoyaltyProgram>()  .HasQueryFilter(e => TenantIds.Contains(e.VenueId) && !e.IsArchived);
    modelBuilder.Entity<Order>()           .HasQueryFilter(e => TenantIds.Contains(e.VenueId));
    modelBuilder.Entity<Purchase>()        .HasQueryFilter(e => TenantIds.Contains(e.VenueId));
    modelBuilder.Entity<Product>()         .HasQueryFilter(e => TenantIds.Contains(e.VenueId) && !e.IsArchived);
    modelBuilder.Entity<LoyaltyProductGroup>().HasQueryFilter(e => TenantIds.Contains(e.LoyaltyProgram.VenueId) && !e.IsArchived);
    modelBuilder.Entity<Worker>()          .HasQueryFilter(e => e.VenueRoles.Any(x => TenantIds.Contains(x.VenueId)) && !e.IsArchived);
}
```

Now `db.Orders.Where(o => o.Status == Ready)` silently becomes
`… WHERE VenueId IN (@tenants) AND Status = …`. The developer *cannot* forget the tenant clause, because
they never write it. Notice the filters also fold in `!IsArchived` — soft-deletion isolation comes free
on the same mechanism (the code even leaves a `//todo` about splitting the archive and tenant concerns
into separate named filters, an honest note about a known limitation).

## Step 4: the gap nobody mentions — writes

Here's the subtlety that most multi-tenancy tutorials skip, and it's the most important paragraph in this
article: **query filters protect reads, not writes.**

Nothing in a query filter stops this:

```csharp
var order = new Order(/* … */);
order.SetVenueId(999);          // a venue I do NOT own
db.Orders.Add(order);
await db.SaveChangesAsync();     // EF happily writes it — no filter runs on INSERT/UPDATE
```

Or worse, loading an entity (bypassing filters via `IgnoreQueryFilters`, or by id from a trusted-looking
path) and mutating it on behalf of another tenant. So the context adds a **write-side guard** that runs on
every save ([`AddAuditInfo`](https://github.com/richardchanjr90-cpu/ddd-by-example/blob/main/src/Loyalty.Infrastructure.DataAccess/Context/LoyaltyTenantDbContext.cs)):

```csharp
public override async Task<int> SaveChangesAsync(CancellationToken ct)
{
    AddAuditInfo();
    return await base.SaveChangesAsync(ct);
}

protected void AddAuditInfo()
{
    // every entity being added/modified must belong to a tenant the caller owns
    var ids = ChangeTracker.Entries()
        .Where(e => e.Entity is ITenantEntity && e.State != EntityState.Unchanged
                    && !(e.State == EntityState.Added && e.Entity is Venue))
        .Select(e => ((ITenantEntity)e.Entity).TenantId)
        .Distinct();

    foreach (var id in ids)
        if (!TenantIds.Contains(id))
            throw new AuthenticationException("Cross-tenant requests are not allowed.");

    // …and while we're here, stamp the audit fields
    foreach (var entry in ChangeTracker.Entries()
                 .Where(e => e.Entity is IAuditableEntity &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified)))
    {
        var auditable = (IAuditableEntity)entry.Entity;
        if (entry.State == EntityState.Added)
        {
            auditable.CreatedBy = provider.Principal?.GetUserId();
            auditable.Created = DateTime.UtcNow;
        }
        auditable.ModifiedBy = provider.Principal?.GetUserId();
        auditable.Modified = DateTime.UtcNow;
    }
}
```

Before *any* save, it walks the change tracker, gathers the `TenantId` of everything being inserted or
updated, and **throws if a single one isn't in the caller's allowed set**. A cross-tenant write can't slip
through, even if a query filter was bypassed upstream. (The `Venue`-on-add exception is the bootstrap
case: when you create a brand-new venue, its tenant *is* itself, which doesn't exist in the token yet.)

The same pass sets `CreatedBy`/`Created`/`ModifiedBy`/`Modified` from the authenticated principal — audit
trails for free, derived from the same identity that enforces isolation. Reads are scoped by the filters,
writes are gated by the guard; isolation holds on both sides.

```
read  path:  query  ──► global query filter appends VenueId IN (tenants)  ──► only my rows
write path:  SaveChanges ──► AddAuditInfo checks every changed entity's TenantId ──► throw on mismatch
```

## The trade-offs

Row-level multi-tenancy is the cheapest to run and the easiest to get *subtly* wrong. Know the edges:

- **Query filters have gotchas.** They apply to the root entity but interact awkwardly with `Include`,
  navigation-based filters (the `LoyaltyProductGroup` filter reaches through `LoyaltyProgram.VenueId`,
  which can hurt query plans), and required relationships. Admin/cross-tenant operations must explicitly
  opt out with `IgnoreQueryFilters()` — which also *removes the safety net*, so guard those paths
  carefully.
- **Derived `TenantId` can bite.** A `LoyaltyGroupRule.TenantId` navigates `LoyaltyProductGroup.TenantId`
  → `LoyaltyProgram.TenantId`; if those navigations aren't loaded at save time, the guard can throw a
  `NullReferenceException` instead of cleanly denying. Derived tenants need their navigation graph present.
- **Shared database, shared blast radius.** One noisy venue's load and one bad migration affect everyone.
  Database-per-tenant trades operational cost for this isolation; pick based on your tenants' size and
  contractual isolation requirements.
- **The guard is only as good as the token.** Everything rests on `GetTenants()` reflecting verified,
  signed claims. If venue claims can be forged or aren't re-validated, the whole edifice is hollow. The
  signed JWT is the root of trust here.
- **Test the negative path.** The valuable test isn't "venue A sees its own orders" — it's "venue A's
  token, asked to write venue B's order, *throws*." Isolation bugs hide in the cases you forget to assert.

## Takeaways

- **Pick a tenancy model deliberately.** Row-level shared tables are cheapest to operate but push all
  isolation responsibility onto your code — so make the code enforce it structurally.
- **Make `TenantId` a required, derived property** on a base type, so no aggregate can exist without
  answering "who owns you?"
- **Resolve the current tenant from the request** (signed claims), behind an `ITenantProvider`
  abstraction, keeping the domain ignorant of HTTP.
- **Isolate reads with EF Core global query filters** so the tenant `WHERE` is automatic and impossible to
  forget.
- **Filters don't cover writes — add a save-time guard** that rejects any changed entity whose `TenantId`
  the caller doesn't own. This is the step most teams miss.
- **Trust flows from the token; test the cross-tenant *denial*, not just the happy path.**

## That's the series

Five parts, one real codebase:

1. [Rich domain models](01-rich-domain-model.md) — behaviour over anemic bags of setters.
2. [The Enumeration state machine](02-state-machine-enumeration.md) — lifecycles that can't go backward.
3. [Domain events & the outbox](03-domain-events-outbox.md) — decoupled, reliably-delivered side effects.
4. [Versioned rules](04-versioned-rules-strategy.md) — business rules that evolve without migrations.
5. **Multi-tenancy** — one database serving many tenants with zero leaks.

None of these are academic. Each solves a problem that bit a real product, and each is sitting in the
[ddd-by-example](https://github.com/richardchanjr90-cpu/ddd-by-example) repository for you to read, clone,
and adapt. DDD isn't the patterns — it's using the patterns, where the complexity actually is, to keep a
real system honest as it grows.

---

*Full source: [ddd-by-example](https://github.com/richardchanjr90-cpu/ddd-by-example). The tenant context
is [`LoyaltyTenantDbContext`](https://github.com/richardchanjr90-cpu/ddd-by-example/blob/main/src/Loyalty.Infrastructure.DataAccess/Context/LoyaltyTenantDbContext.cs);
the tenant resolver is [`TenantTokenProvider`](https://github.com/richardchanjr90-cpu/ddd-by-example/blob/main/src/Loyalty.Infrastructure.DataAccess/TenantTokenProvider.cs).*

---

*Written by **Richard Chan** — I build and write about Domain-Driven Design and clean architecture in .NET. This is part 5 of a five-part series; the complete, runnable source is open-source at [github.com/richardchanjr90-cpu/ddd-by-example](https://github.com/richardchanjr90-cpu/ddd-by-example). If it helped, a clap and a follow help others find it.*
