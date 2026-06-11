# Architecture — a layer-by-layer tour

This document walks the solution from the inside out, following the **dependency rule**: code in an
inner layer never references an outer one. If you read it top to bottom you'll have a mental model of
where every responsibility lives and why.

```
Infrastructure  ─────►  Application  ─────►  Domain core
 (EF, Azure,            (use cases,          (aggregates, value
  Service Bus)          handlers)             objects, events)
```

The domain core is the center. It is made of plain C# objects with **no dependency on EF Core, Azure,
HTTP, or MediatR handlers** — the only external package it touches is `MediatR`'s `INotification`
marker, used to type domain events.

---

## 1. Domain core — `Loyalty.Core.Entities`

The heart of the system. Everything else exists to serve it.

### SeedWork — the shared DDD base types

[`SeedWork/`](../src/Loyalty.Core.Entities/SeedWork) holds the reusable tactical primitives, modelled
after the building blocks Eric Evans describes and Microsoft's reference apps popularised:

- [`Entity`](../src/Loyalty.Core.Entities/SeedWork/Entity.cs) — identity-based equality, transient
  detection, and a private list of domain events with `AddDomainEvent` / `ClearDomainEvents`.
- [`ValueObject`](../src/Loyalty.Core.Entities/SeedWork/ValueObject.cs) — structural equality based on
  `GetAtomicValues()`.
- [`Enumeration`](../src/Loyalty.Core.Entities/SeedWork/Enumeration.cs) — the "smart enum" base used to
  build the order state machine.
- [`Interfaces/`](../src/Loyalty.Core.Entities/SeedWork/Interfaces) — `IAggregateRoot`, `IRepository`,
  `IUnitOfWork`. `IAggregateRoot` is a marker that documents which entities are consistency boundaries.

### Aggregates

Each folder under [`Aggregates/`](../src/Loyalty.Core.Entities/Aggregates) is one consistency boundary,
entered only through its root:

| Aggregate root | Responsibility | Notable invariant |
|----------------|----------------|-------------------|
| [`LoyaltyProgram`](../src/Loyalty.Core.Entities/Aggregates/LoyaltyPrograms/LoyaltyProgram.cs) | A venue's loyalty program and its product groups + rules | Cannot be edited once `IsPublished` |
| [`Order`](../src/Loyalty.Core.Entities/Aggregates/Orders/Order.cs) | A customer order and its items | Status only moves forward (state machine) |
| [`Venue`](../src/Loyalty.Core.Entities/Aggregates/Venues/Venue.cs) | A business location, its contact info and details | Holds value objects, not loose fields |
| [`Product`](../src/Loyalty.Core.Entities/Aggregates/Products/Product.cs) | A purchasable/redeemable product | Archival cascades sensibly |
| [`Purchase`](../src/Loyalty.Core.Entities/Aggregates/Purchases/Purchase.cs) | A point-earning or point-burning transaction | Created only via `Assign`/`Burn` factories |
| [`Worker`](../src/Loyalty.Core.Entities/Aggregates/Workers/Worker.cs) | A staff member and their venue links | — |

Aggregates expose **behaviour, not setters**. Child collections are exposed as
`IReadOnlyCollection<T>` and mutated only through root methods like
`LoyaltyProgram.AddLoyaltyGroup(...)`.

### Value Objects

[`Venues/ValueObjects/`](../src/Loyalty.Core.Entities/Aggregates/Venues/ValueObjects) — `Location`,
`ContactInfo`, `SocialNetworks`, `VenueDetails`. Immutable, identity-free, compared by value.

### The order state machine

[`Orders/Status/`](../src/Loyalty.Core.Entities/Aggregates/Orders/Status) models the order lifecycle as
eight `Enumeration` subclasses. Each overrides `Set(Order)` to allow or reject a transition, so illegal
moves are impossible by construction rather than guarded by scattered `if` statements. This is the
subject of [article 2](articles/02-state-machine-enumeration.md).

### Domain events

[`Events/`](../src/Loyalty.Core.Entities/Events) holds past-tense records like
`LoyaltyProgramCreatedDomainEvent` and `OrderStatusChangedDomainEvent`. They are raised *inside*
aggregate methods (`AddDomainEvent(new …)`) and live on the entity until the unit of work dispatches
them. See [article 3](articles/03-domain-events-outbox.md).

### Rules

[`Rules/`](../src/Loyalty.Core.Entities/Rules) models loyalty earning rules (`StampsRuleV1`,
`PercentFixedRuleV1`, `PercentThatDependsOnTotalSumRuleV1`) as a versioned strategy family, with an
`ICompatibleRule` notion of which rules can coexist.

---

## 2. Application layer

This layer expresses **use cases**. It holds *no* business rules — it loads aggregates, calls their
methods, and commits.

- [`Loyalty.Application.Venue`](../src/Loyalty.Application.Venue) — application services such as
  [`OrderAppService`](../src/Loyalty.Application.Venue/OrderAppService.cs) and
  `LoyaltyProgramAppService`. Each is thin: it translates a view model into a MediatR command/query and
  returns the result.
- [`Loyalty.Application.ViewModels`](../src/Loyalty.Application.ViewModels) — request/response shapes and
  their **FluentValidation** validators.
- `Loyalty.Domain.Handlers.Queries` / `Loyalty.Infrastructure.Handlers.Commands` — the **CQRS** split:
  commands mutate aggregates through repositories; queries read (sometimes via Dapper) without touching
  the write model.
- [`Loyalty.Application.DomainEvents.Handlers`](../src/Loyalty.Application.DomainEvents.Handlers) —
  handlers that react to domain events (e.g., turning a `PurchaseMadeEvent` into downstream work).

The flow for a write is:

```
HTTP trigger → AppService → MediatR command → CommandHandler
   → repository.Load(aggregate) → aggregate.DoSomething() (raises domain events)
   → unitOfWork.SaveEntitiesAsync() → dispatch domain events → SaveChanges
```

---

## 3. Infrastructure

The outer ring — everything that talks to the world.

### Persistence — `Loyalty.Infrastructure.DataAccess`

- [`LoyaltyDbContext`](../src/Loyalty.Infrastructure.DataAccess/Context/LoyaltyDbContext.cs) — the EF Core
  context. Its `SaveEntitiesAsync` **dispatches domain events before saving**, the deferred-dispatch
  pattern from [article 3](articles/03-domain-events-outbox.md).
- [`EntityConfigurations/`](../src/Loyalty.Infrastructure.DataAccess/EntityConfigurations) — all EF mapping
  via `IEntityTypeConfiguration<T>`, keeping persistence concerns *out* of the domain. Highlights:
  - [`VenueConfiguration`](../src/Loyalty.Infrastructure.DataAccess/EntityConfigurations/VenueConfiguration.cs)
    maps value objects with `OwnsOne(...)`.
  - [`OrderConfiguration`](../src/Loyalty.Infrastructure.DataAccess/EntityConfigurations/OrderConfiguration.cs)
    persists the `OrderStatusEnumeration` state machine with a value converter
    (`HasConversion(v => v.Id, v => OrderStatusEnumeration.From(v))`).
- [`MediatorExtension`](../src/Loyalty.Infrastructure.DataAccess/MediatorExtension.cs) — the
  `DispatchDomainEventsAsync` extension that drains events off tracked entities and publishes them.

### Reliable messaging — `Loyalty.Core.Outbox.Entities` + `Loyalty.Infrastructure.Outbox`

- [`IntegrationEventLogEntry`](../src/Loyalty.Core.Outbox.Entities/IntegrationEventLogEntry.cs) — an event
  serialized to JSON with a `State` (`NotPublished` → `Published`/`PublishedFailed`) and the
  transaction id that produced it.
- [`PersistentIntegrationEventService`](../src/Loyalty.Infrastructure.Outbox/PersistentIntegrationEventService.cs)
  — writes the event into the **same database transaction** as the business change, then a relay
  publishes it to Azure Service Bus and marks it published. This is the **transactional outbox** that
  defeats the dual-write problem.

### Other adapters

- `Loyalty.Infrastructure.Firebase.Handlers` — Firebase auth/notification integration.
- `Loyalty.Infrastructure.IoC` — the dependency-injection composition (`*Extensions` registering EF,
  Service Bus, repositories, MediatR).
- `Loyalty.Infrastructure.Logging.AppInsights` — telemetry.

---

## 4. Host — `LoyaltyProgram` (Azure Functions)

The composition root. Functions are thin transport adapters grouped by intent:

- `Http/Read/*` and `Http/Write/*` — HTTP-triggered read/write endpoints.
- `ServiceBus/*`, `Storage/*` — queue and Service Bus triggers that react to integration events and
  background work.
- `SendGrid/*` — outbound email.

A function's job is to bind the request, hand it to an application service, and shape the response —
nothing more.

---

## Cross-cutting: multi-tenancy

[`TenantEntity`](../src/Loyalty.Core.Entities/Base/TenantEntity.cs) gives every tenant-scoped aggregate a
`TenantId` (usually the `VenueId`), and [`ITenantProvider`](../src/Loyalty.Core.Contracts/ITenantProvider.cs)
supplies the current tenant so queries and writes stay isolated per venue.

---

## Where to go next

- Read the [article series](articles/README.md) in order — each one zooms into one slice of this map.
- Or start from a single aggregate ([`LoyaltyProgram`](../src/Loyalty.Core.Entities/Aggregates/LoyaltyPrograms/LoyaltyProgram.cs))
  and follow its events outward.
