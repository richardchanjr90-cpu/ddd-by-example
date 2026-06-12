# Stop Writing Anemic Domain Models: The Building Blocks of DDD in C#

> ### A code-first tour of entities, value objects, and aggregates — built from a real loyalty-program backend, not a toy Order/Customer.

*Part 1 of 5 in “Domain-Driven Design by Example” · ~11 min read*

**Suggested Medium tags:** Domain Driven Design, Dotnet, CSharp, Software Architecture, Programming

> 🧩 Part of an open-source series — every code link points to the real file in the [ddd-by-example](https://github.com/richardchanjr90-cpu/ddd-by-example) repository, a production loyalty-program backend (not a toy example). Diagrams are embedded as images so they render directly here.

---
You've seen this class. Maybe you wrote it last week:

```csharp
public class LoyaltyProgram
{
    public long Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public bool IsPublished { get; set; }
    public List<LoyaltyProductGroup> Groups { get; set; }
}
```

Every property is public. Every setter is open. The class knows nothing about the rules of the business
it claims to model. So where do the rules live? In a `LoyaltyProgramService` 600 lines long, where every
method starts by re-checking the same things: *is it published? does it have groups? are the dates
valid?* — because nothing stops a caller from putting the object into a nonsense state.

Martin Fowler has a name for this: the [**Anemic Domain Model**](https://martinfowler.com/bliki/AnemicDomainModel.html).
It looks object-oriented — nouns, properties, relationships — but it's procedural code wearing an OO
costume. The objects are "bags of getters and setters" and all the behaviour has leaked into service
classes. You get the cost of objects with none of the benefit.

This series fixes that, using a **real codebase**: the backend of a loyalty-and-ordering platform
([full source here](https://github.com/richardchanjr90-cpu/ddd-by-example)). Venues run loyalty
programs, customers earn and burn points, orders move through a lifecycle. It has genuine invariants —
which is exactly what makes it a good teacher. In this first part we'll cover the tactical building
blocks of Domain-Driven Design and use them to turn that anemic class into a model that protects itself.

## First, the part everyone skips: ubiquitous language

Before any pattern, DDD asks one thing: **name things the way the business names them, everywhere —
in conversation and in code.** Eric Evans calls this the *ubiquitous language*, and Vernon and Fowler
both argue it's the actual core of DDD; the tactical patterns (the classes we're about to build) exist
to *serve* the language, not the other way around.

In our domain, the words are concrete: a **Venue** runs a **LoyaltyProgram** made of
**LoyaltyProductGroups**, each governed by **Rules**. A customer makes a **Purchase** that either
*assigns* or *burns* points. An **Order** is **Placed**, then **Started**, **Ready**, **Finished**.
Open the codebase and those are the exact class names — [`LoyaltyProgram`](https://github.com/richardchanjr90-cpu/ddd-by-example/blob/main/src/Loyalty.Core.Entities/Aggregates/LoyaltyPrograms/LoyaltyProgram.cs),
[`Purchase`](https://github.com/richardchanjr90-cpu/ddd-by-example/blob/main/src/Loyalty.Core.Entities/Aggregates/Purchases/Purchase.cs),
[`Order`](https://github.com/richardchanjr90-cpu/ddd-by-example/blob/main/src/Loyalty.Core.Entities/Aggregates/Orders/Order.cs). When a product manager says
"you can't edit a published program," there is one method, in one place, that says exactly that. That
alignment is the whole game.

## Entities: identity over attributes

The first building block. An **Entity** is a thing defined by its *identity and continuity*, not by its
field values. Two customers named "John Smith" are different people; a venue that changes its address is
still the same venue. Identity is what matters, so equality is based on identity — not on comparing every
property.

Here's the base [`Entity`](https://github.com/richardchanjr90-cpu/ddd-by-example/blob/main/src/Loyalty.Core.Entities/SeedWork/Entity.cs) every aggregate in the
codebase inherits from (trimmed):

```csharp
public abstract class Entity
{
    public virtual long Id { get; protected set; }

    public bool IsTransient() => Id == default;

    public override bool Equals(object obj)
    {
        if (obj is not Entity item) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (GetType() != obj.GetType()) return false;
        if (item.IsTransient() || IsTransient()) return false;
        return item.Id == Id;          // identity, not attributes
    }
    // GetHashCode based on Id, == / != operators, domain-event list…
}
```

Two details worth noticing. `Id` has a `protected set` — the outside world can't assign identity. And
the equality rule treats *transient* entities (not yet persisted, `Id == 0`) as never equal, because
they don't have an identity yet. This base type lives in a folder called **SeedWork** — the small set of
reusable DDD primitives, a convention Microsoft's reference apps popularised.

## Value Objects: when identity doesn't matter

Not everything has an identity. A venue's **location** — city, address, latitude, longitude — has no
"who"; it's just a value. If two venues have the exact same coordinates and address, those locations
*are* the same location. That's a **Value Object**: immutable, identity-free, and compared by value.

[`Location`](https://github.com/richardchanjr90-cpu/ddd-by-example/blob/main/src/Loyalty.Core.Entities/Aggregates/Venues/ValueObjects/Location.cs):

```csharp
public class Location : ValueObject
{
    public Location(string city, string address, float? latitude, float? longitude)
    {
        City = city;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
    }

    public string City { get; private set; }
    public string Address { get; private set; }
    public float? Latitude { get; private set; }
    public float? Longitude { get; private set; }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return City;
        yield return Address;
        yield return Latitude;
        yield return Longitude;
    }
}
```

The whole class is set through the constructor and never mutated. The base
[`ValueObject`](https://github.com/richardchanjr90-cpu/ddd-by-example/blob/main/src/Loyalty.Core.Entities/SeedWork/ValueObject.cs) implements equality by walking
`GetAtomicValues()` — so two `Location`s are equal when *all their components* are equal, exactly the
semantics [Fowler describes](https://martinfowler.com/bliki/ValueObject.html).

Why bother? Because a Value Object is a magnet for behaviour and validation. Instead of a `Venue` with
four loose `string City`, `string Address`, `float Lat`, `float Lng` fields that any service can scramble
independently, you have one cohesive concept that's always internally consistent. The codebase does the
same for [`ContactInfo`](https://github.com/richardchanjr90-cpu/ddd-by-example/blob/main/src/Loyalty.Core.Entities/Aggregates/Venues/ValueObjects/ContactInfo.cs)
and [`SocialNetworks`](https://github.com/richardchanjr90-cpu/ddd-by-example/blob/main/src/Loyalty.Core.Entities/Aggregates/Venues/ValueObjects/SocialNetworks.cs).

## Aggregates: the consistency boundary

![The LoyaltyProgram aggregate — callers reach children only through the root](https://mermaid.ink/img/Zmxvd2NoYXJ0IFRCCiAgc3ViZ3JhcGggQUdHWyJMb3lhbHR5UHJvZ3JhbSDigJQgQWdncmVnYXRlIFJvb3QiXQogICAgUlsiTG95YWx0eVByb2dyYW08YnIvPklzUHVibGlzaGVkLCBkYXRlcywgVXJsIl0KICAgIEcxWyJMb3lhbHR5UHJvZHVjdEdyb3VwIl0KICAgIEcyWyJMb3lhbHR5R3JvdXBSdWxlICh2YWx1ZSBvYmplY3QpIl0KICAgIFIgLS0-fG93bnN8IEcxCiAgICBHMSAtLT58b3duc3wgRzIKICBlbmQKICBFWFRbIkFwcGxpY2F0aW9uIC8gY2FsbGVycyJdIC0tPnwib25seSB2aWEgcm9vdCBtZXRob2RzPGJyLz5VcGRhdGUoKSwgQWRkTG95YWx0eUdyb3VwKCkifCBSCiAgUiAtLiAicmVmZXJlbmNlcyBieSBpZCIgLi0-IFBHWyJQcm9kdWN0R3JvdXAgKG90aGVyIGFnZ3JlZ2F0ZSkiXQo=?type=png)

*The LoyaltyProgram aggregate — callers reach children only through the root.*


This is the building block that pays the rent.

An **Aggregate** is a cluster of entities and value objects that must stay consistent *together*, treated
as a single unit. One entity is the **Aggregate Root** — the only entry point. You never reach inside and
poke a child object; you go through the root, and the root enforces the invariants. Vaughn Vernon calls
the root the *consistency guardian*.

Our `LoyaltyProgram` is an aggregate root. It owns a collection of `LoyaltyProductGroup`s, and it has a
real business rule: **once a program is published, it's frozen.** Watch how that rule is *impossible to
violate* because the data and the behaviour live together
([`LoyaltyProgram`](https://github.com/richardchanjr90-cpu/ddd-by-example/blob/main/src/Loyalty.Core.Entities/Aggregates/LoyaltyPrograms/LoyaltyProgram.cs)):

```csharp
public class LoyaltyProgram : TenantEntity, IAggregateRoot
{
    public string Name { get; private set; }              // note: private set
    public bool IsPublished { get; private set; }

    private readonly List<LoyaltyProductGroup> loyaltyProductGroups;
    public IReadOnlyCollection<LoyaltyProductGroup> LoyaltyProductGroups => loyaltyProductGroups;

    public void Update(string name, string description, DateTime startDate,
                       DateTime? endDate, bool isPublished, Uri url)
    {
        if (IsPublished)
            throw new LoyaltyValidationException(
                "You can't change already published program.", ErrorCode.IS_PUBLISHED);

        Name = name;
        // …
        AddDomainEvent(new LoyaltyProgramUpdatedDomainEvent(this));
    }

    public void AddLoyaltyGroup(LoyaltyProductGroup item)
    {
        loyaltyProductGroups.Add(item);
        AddDomainEvent(new LoyaltyGroupCreatedDomainEvent(item));
    }
}
```

Three things make this a rich model rather than an anemic one:

1. **Every setter is `private set`.** State changes only through intention-revealing methods (`Update`,
   `Archive`, `AddLoyaltyGroup`), never by external assignment.
2. **The collection is exposed as `IReadOnlyCollection`.** No caller can do `program.Groups.Add(...)` and
   skip the root. To add a group you call `AddLoyaltyGroup`, and the root gets to react (here, by raising
   a domain event — more on those in Part 3).
3. **The invariant is enforced at the point of change.** `Update` throws if the program is published.
   There is no path to an invalid state. The 600-line service that re-checked everything everywhere is
   gone, because the object can no longer *be* wrong.

### Reference other aggregates by ID, not by navigation

A subtle but important rule from Vernon's *Effective Aggregate Design*: keep aggregates **small**, and
reference *other* aggregates **by identity**, not by holding the whole object. Look at how a
[`LoyaltyProductGroup`](https://github.com/richardchanjr90-cpu/ddd-by-example/blob/main/src/Loyalty.Core.Entities/Aggregates/LoyaltyPrograms/LoyaltyProductGroup.cs)
links to a product group:

```csharp
public LoyaltyProductGroup(string name, ProductGroup productGroup, string description,
                           long programVenueId, List<LoyaltyGroupRule> rules)
{
    if (productGroup == null)
        throw new LoyaltyValidationException("No product group with provided id was found.", …);

    if (programVenueId != productGroup.VenueId)
        throw new LoyaltyValidationException("Product Group and Program belong to different venues.", …);

    if (rules == null || !rules.Any())
        throw new LoyaltyValidationException("No rule Specified", …);

    Name = name;
    ProductGroupId = productGroup.Id;   // store the ID, not the object
    // …
}
```

The constructor takes the `ProductGroup` only to *validate* it (it must exist, and it must belong to the
same venue), then stores `ProductGroupId`. The `Product` aggregate and the `LoyaltyProgram` aggregate
stay separate consistency boundaries — they don't drag each other into one giant object graph. Big
aggregates are the most common DDD beginner mistake: they cause lock contention and make invariants
impossible to reason about. Keep them small.

Notice too that the constructor is a wall of guard clauses. An aggregate is never halfway valid — if you
can construct one, it's correct.

## Factory methods: making intent explicit

Sometimes "new it up" doesn't capture intent. A [`Purchase`](https://github.com/richardchanjr90-cpu/ddd-by-example/blob/main/src/Loyalty.Core.Entities/Aggregates/Purchases/Purchase.cs)
in our domain can mean two opposite things: a customer *earning* points or *spending* (burning) them.
Same data shape, opposite meaning. A constructor called `new Purchase(...)` can't express that — so the
aggregate hides its constructor and exposes named **factory methods**:

```csharp
public static Purchase Assign(string workerId, long groupId, long venueId,
                              long? productId, string userId, decimal? value)
    => new Purchase(workerId, groupId, venueId, productId, userId, value);

public static Purchase Burn(string workerId, long groupId, long venueId,
                            long? productId, string userId, decimal? value)
    => new Purchase(workerId, groupId, venueId, productId, userId, -value);   // note the sign

private Purchase(/* … */)
{
    // …
    if (value > 0) AddDomainEvent(new PurchaseMadeEvent(this, workerId));
    else if (value < 0) AddDomainEvent(new PurchaseBurnedEvent(this, workerId));
}
```

`Purchase.Burn(...)` reads like the business talks, flips the sign so a burn is a negative value, and
raises the right domain event. The *only* ways to create a `Purchase` are the two that make business
sense. That's the ubiquitous language showing up as API surface.

## Where do the rules *not* go: the application layer stays thin

If the aggregate holds the rules, what's left for the service layer? **Orchestration, and nothing else.**
An *Application Service* (or command handler) loads the aggregate, calls a method on it, and commits the
unit of work. Evans is explicit that the application layer is "kept thin… it does not contain business
rules." Here's the actual handler that creates a program
([`CreateLoyaltyProgramCommandHandler`](https://github.com/richardchanjr90-cpu/ddd-by-example/blob/main/src/Loyalty.Infrastructure.Handlers.Commands/Commands/LoyaltyPrograms/CreateLoyaltyProgramCommandHandler.cs)):

```csharp
public async Task<ICommandResult> Handle(CreateLoyaltyProgramCommand request, CancellationToken ct)
{
    var program = new LoyaltyProgram(
        request.Name, request.Description, request.StartDate,
        request.EndDate, request.VenueId, request.ExternalProgramUri);

    await programRepository.AddAsync(program);

    return new CommandResult
    {
        Success = await programRepository.UnitOfWork.SaveEntitiesAsync(ct),
        Result = program.Id
    };
}
```

No `if` statements about business rules. No validation of the published state. It builds the aggregate,
hands it to a [repository](https://github.com/richardchanjr90-cpu/ddd-by-example/blob/main/src/Loyalty.Core.Entities/Interfaces/Repository/ILoyaltyProgramRepository.cs)
(an in-memory-collection abstraction over persistence — **one per aggregate root**, never one per table),
and saves. All the *meaning* lives in the domain.

## The trade-offs (when *not* to do this)

DDD's tactical patterns are not free, and the honest position — the one Microsoft's own guidance takes —
is that they're **overkill for simple CRUD**. If a "loyalty program" were just a form that writes a row
with no rules, an anemic class plus a thin service is the *right* call; wrapping it in aggregates and
value objects would be ceremony with no payoff.

Reach for the rich model when the domain has **invariants worth protecting** — states that must not be
entered, transitions that must not happen, consistency that spans several objects. The `IsPublished`
freeze, the same-venue check on product groups, the earn-vs-burn distinction: *that's* the complexity
that earns the patterns. Apply them where the rules are, and stay lightweight everywhere else. Use DDD as
a scalpel, not a religion.

## Takeaways

- **Ubiquitous language first.** The patterns serve the language. If your class names don't match how the
  business talks, fix that before anything else.
- **Entities are identity; Value Objects are value.** Pick deliberately, and let Value Objects absorb
  validation and cohesion.
- **The aggregate root is the consistency guardian.** `private set`, `IReadOnlyCollection`, behaviour
  methods, guard clauses — make invalid states unrepresentable.
- **Keep aggregates small; reference others by ID.** It's the difference between a model and a
  distributed lock.
- **The application layer stays thin.** Rules live in the domain; services just orchestrate.
- **Don't cargo-cult it.** No invariants, no aggregate. DDD is for the complex core, not the CRUD edges.

In **Part 2**, we take the order lifecycle — Placed → Started → Ready → Finished — and replace the
classic `switch`-on-`enum` mess with a real state machine using the **Enumeration-class pattern**, so
that an order can move forward but *never* backward.
[Read Part 2 →](02-state-machine-enumeration.md)

---

*The complete, runnable source for every snippet above is in the
[ddd-by-example](https://github.com/richardchanjr90-cpu/ddd-by-example) repository. If this was useful,
the repo's README maps every DDD pattern to the file that implements it.*

---

*Written by **Richard Chan** — I build and write about Domain-Driven Design and clean architecture in .NET. This is part 1 of a five-part series; the complete, runnable source is open-source at [github.com/richardchanjr90-cpu/ddd-by-example](https://github.com/richardchanjr90-cpu/ddd-by-example). If it helped, a clap and a follow help others find it.*
