# Domain-Driven Design by Example — the article series

Three standalone, publishable articles that teach DDD using this real loyalty-program backend as the
single running example. Read them in order; each builds on the last.

| # | Article | Read it for | Est. read |
|---|---------|-------------|-----------|
| 1 | [Stop Writing Anemic Domain Models](01-rich-domain-model.md) | The tactical building blocks — ubiquitous language, entities, value objects, aggregates, and putting behaviour back into your model | ~11 min |
| 2 | [Your `enum` Is a Code Smell](02-state-machine-enumeration.md) | Modelling a real lifecycle as a state machine with the Enumeration-class pattern, and persisting it cleanly | ~10 min |
| 3 | [Domain Events & the Dual-Write Bug](03-domain-events-outbox.md) | Decoupling side effects with domain events, and guaranteeing delivery with a transactional outbox | ~12 min |
| 4 | [Versioning Business Rules](04-versioned-rules-strategy.md) | Modelling volatile business rules as versioned strategies that evolve without data migrations | ~10 min |
| 5 | [Multi-Tenancy in EF Core](05-multitenancy-query-filters.md) | One database serving many tenants with zero leaks — global query filters for reads, a write-side guard for writes | ~11 min |

> **Publishing to Medium?** Ready-to-paste versions of every part — with embedded diagram images and
> absolute repo links — live in [`medium/`](medium/).

## How these are meant to be used

- Each `.md` is a **ready-to-publish Medium draft**: a hook, the problem, the concept, real code with
  links, the trade-offs, and takeaways.
- Every code snippet links to the actual file in this repo so readers can see the pattern *in context*,
  not as a toy.
- Diagrams are written in **Mermaid** so they render on GitHub and can be exported for Medium.

## Publishing checklist (per article)

- [ ] Paste into Medium, re-upload the Mermaid diagrams as images (Medium doesn't render Mermaid).
- [ ] Replace relative repo links with absolute `https://github.com/<owner>/ddd-by-example/...` links.
- [ ] Add a canonical-link back to the repo and to the previous/next part.
- [ ] Tag: `Domain Driven Design`, `Software Architecture`, `Dotnet`, `CSharp`, `Programming`.

## Sources & further reading

These articles lean on the canonical DDD literature. The most useful primary sources:

- Microsoft — [Designing a DDD-oriented microservice](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice)
  and [Designing a microservice domain model](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/microservice-domain-model)
- Microsoft — [Domain events: design and implementation](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/domain-events-design-implementation)
  and [Enumeration classes over enum types](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types)
- Martin Fowler — [Anemic Domain Model](https://martinfowler.com/bliki/AnemicDomainModel.html),
  [Value Object](https://martinfowler.com/bliki/ValueObject.html),
  [Bounded Context](https://martinfowler.com/bliki/BoundedContext.html)
- Vaughn Vernon — [Effective Aggregate Design (I–III)](https://www.dddcommunity.org/library/vernon_2011/)
- Jimmy Bogard — [A better domain events pattern](https://lostechies.com/jimmybogard/2014/05/13/a-better-domain-events-pattern/)
- Steve "Ardalis" Smith — [SmartEnum](https://github.com/ardalis/SmartEnum)
- Chris Richardson — [Transactional Outbox pattern](https://microservices.io/patterns/data/transactional-outbox.html)
- Reference apps — [dotnet/eShop](https://github.com/dotnet/eShop) (current),
  [eShopOnContainers](https://github.com/dotnet-architecture/eShopOnContainers) (the classic original)
