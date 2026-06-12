# Versioning Business Rules: A Strategy Pattern That Survives Change

> ### Model volatile business rules as versioned strategies — persisted as tagged JSON — that evolve with zero data migrations.

*Part 4 of 5 in “Domain-Driven Design by Example” · ~10 min read*

**Suggested Medium tags:** Domain Driven Design, Dotnet, CSharp, Software Architecture, Design Patterns

> 🧩 Part of an open-source series — every code link points to the real file in the [ddd-by-example](https://github.com/richardchanjr90-cpu/ddd-by-example) repository, a production loyalty-program backend (not a toy example). Diagrams are embedded as images so they render directly here.

---
Of everything in a real product, **business rules change the most.** In our loyalty platform, a venue
might reward customers with stamps ("buy 9 coffees, get the 10th free"), or a flat percentage back, or a
tiered percentage that grows with the total spend — and next quarter marketing invents three more. The
rules also *combine*: a group might run stamps *and* a birthday bonus together, but stamps *and* a flat
percentage might be nonsense.

If you model that with a column per rule type and a wall of `if`s in a service, every new promotion is a
schema migration plus a deploy, and every change risks breaking the programs already running in
production. We need rules that are **pluggable** (add a new kind without touching the old ones) and
**evolvable** (change a rule's shape without breaking the millions of rows already saved in the old
shape). That's a job for the **Strategy pattern** plus deliberate **versioning** — and it's how the
[ddd-by-example](https://github.com/richardchanjr90-cpu/ddd-by-example) codebase does it.

## Each rule is a small strategy

Start with one class per kind of rule, each carrying only the data *that* rule needs. They share a tiny
base, [`BaseRule`](https://github.com/richardchanjr90-cpu/ddd-by-example/blob/main/src/Loyalty.Core.Entities/Rules/ICompatibleRule.cs):

```csharp
public abstract class BaseRule
{
    public abstract List<LoyaltyRuleType> CompatibleRules { get; set; }
}
```

And the concrete strategies ([`StampsRuleV1`](https://github.com/richardchanjr90-cpu/ddd-by-example/blob/main/src/Loyalty.Core.Entities/Rules/StampsRuleV1.cs),
[`PercentFixedRuleV1`](https://github.com/richardchanjr90-cpu/ddd-by-example/blob/main/src/Loyalty.Core.Entities/Rules/PercentFixedRuleV1.cs),
[`PercentThatDependsOnTotalSumRuleV1`](https://github.com/richardchanjr90-cpu/ddd-by-example/blob/main/src/Loyalty.Core.Entities/Rules/PercentThatDependsOnTotalSumRuleV1.cs)):

```csharp
public class StampsRuleV1 : BaseRule
{
    [JsonPropertyName("stampsToCollect")] public int StampsToCollect { get; set; }
    [JsonPropertyName("stampsToAssign")]  public int StampsToAssign { get; set; } = 0;
    public override List<LoyaltyRuleType> CompatibleRules { get; set; }
}

public class PercentFixedRuleV1 : BaseRule
{
    public double FixedPercent { get; set; }
    public override List<LoyaltyRuleType> CompatibleRules { get; set; }
}

public class PercentThatDependsOnTotalSumRuleV1 : BaseRule
{
    public List<PercentRange> Ranges { get; set; } = new();   // tiered: spend more, earn more
    public override List<LoyaltyRuleType> CompatibleRules { get; set; } = new();
}
```

Each is self-contained. Adding a "birthday present" rule means adding a `BirthDayPresentRuleV1` class —
**no existing rule changes**, which is the open/closed principle paying off. The shapes are genuinely
different (a count, a single percent, a list of ranges), and that's the point: a column-per-field schema
could never represent all of them cleanly.

## The discriminator: type and compatibility as a `[Flags]` enum

How does the system know *which* strategy a stored rule is? A discriminator —
[`LoyaltyRuleType`](https://github.com/richardchanjr90-cpu/ddd-by-example/blob/main/common/Loyalty.Shared.Contracts/Enums/LoyaltyRuleType.cs) — and it's a `[Flags]`
enum, which does double duty:

```csharp
[Flags]
public enum LoyaltyRuleType
{
    Stamps = 1,
    PercentFixed = 2,
    PercentToDate = 4,
    PercentThatDependsOnPeriod = 8,
    PercentForActionsDone = 16,
    PercentThatDependsOnTotalSum = 32,
    BringAFriend = 64,
    BirthDayPresent = 256
}
```

Because the values are powers of two, `CompatibleRules` can express *which rules may coexist* in one
group as a set of flags. The "stamps and birthday-bonus together, but not stamps and flat-percent"
business constraint becomes data on the rule, not branching logic scattered across the app.

## Persisting a polymorphic strategy: serialize, tag, version

![A rule stored as type+version-tagged JSON, rehydrated into the right strategy](https://mermaid.ink/img/Zmxvd2NoYXJ0IFRCCiAgVUlbIkFkbWluIGRlZmluZXMgYSBydWxlIl0gLS0-IExHUlsiTG95YWx0eUdyb3VwUnVsZSAodmFsdWUgb2JqZWN0KSJdCiAgTEdSIC0tPiBUWyJSdWxlVHlwZTogU3RhbXBzIHwgUGVyY2VudEZpeGVkIHwgLi4uIl0KICBMR1IgLS0-IFZbIlJ1bGVWZXJzaW9uOiBWMSJdCiAgTEdSIC0tPiBKWyJSdWxlOiBzZXJpYWxpemVkIEpTT04iXQogIEogLS0-fCJkZXNlcmlhbGl6ZSBieSAoVHlwZSwgVmVyc2lvbikifCBTe1N0cmF0ZWd5fQogIFMgLS0-IFMxWyJTdGFtcHNSdWxlVjEiXQogIFMgLS0-IFMyWyJQZXJjZW50Rml4ZWRSdWxlVjEiXQogIFMgLS0-IFMzWyJQZXJjZW50VGhhdERlcGVuZHNPblRvdGFsU3VtUnVsZVYxIl0K?type=png)

*A rule stored as type+version-tagged JSON, rehydrated into the right strategy.*


Here's the crux. These strategies have *different shapes*, but they all live in the same place — a
group's list of rules. Relational tables hate polymorphism. So the model stores each rule as **serialized
JSON, tagged with its type and a version**, wrapped in a value object,
[`LoyaltyGroupRule`](https://github.com/richardchanjr90-cpu/ddd-by-example/blob/main/src/Loyalty.Core.Entities/Aggregates/LoyaltyPrograms/LoyaltyGroupRule.cs):

```csharp
public class LoyaltyGroupRule : ValueObject
{
    public LoyaltyGroupRule(LoyaltyRuleType ruleType, object rule, LoyaltyRuleVersion ruleVersion)
    {
        RuleType = ruleType;
        Rule = JsonSerializer.Serialize(rule);   // the strategy → JSON
        RuleVersion = ruleVersion;
    }

    public LoyaltyRuleType RuleType { get; private set; }      // which strategy
    public string Rule { get; private set; }                   // its data, as JSON
    public LoyaltyRuleVersion RuleVersion { get; private set; } // which shape of that strategy

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return LoyaltyProductGroupId;
        yield return RuleType;
        yield return Rule;
        yield return RuleVersion;
    }
}
```

This is "schema-on-read" for behaviour configuration. The database column is just `nvarchar`; the
*meaning* of that JSON is recovered at read time from the `(RuleType, RuleVersion)` pair. Note it's a
**Value Object** (from [Part 1](01-rich-domain-model.md)) — a rule has no identity of its own; it's
defined entirely by its values, and two identical rules are equal.

To turn a stored row back into a usable strategy, you deserialize by type and version:

```csharp
BaseRule Rehydrate(LoyaltyGroupRule stored) => (stored.RuleType, stored.RuleVersion) switch
{
    (LoyaltyRuleType.Stamps, LoyaltyRuleVersion.V1) =>
        JsonSerializer.Deserialize<StampsRuleV1>(stored.Rule),
    (LoyaltyRuleType.PercentFixed, LoyaltyRuleVersion.V1) =>
        JsonSerializer.Deserialize<PercentFixedRuleV1>(stored.Rule),
    // …one arm per (type, version)
};
```

## The part that earns the title: versioning

Look again at the names — `StampsRuleV1`, `PercentFixedRuleV1` — and the
[`LoyaltyRuleVersion`](https://github.com/richardchanjr90-cpu/ddd-by-example/blob/main/common/Loyalty.Shared.Contracts/Enums/LoyaltyRuleVersion.cs) enum:

```csharp
public enum LoyaltyRuleVersion { V1 = 1 }
```

That `V1` looks like over-engineering until the day marketing changes how stamps work — say, stamps now
expire after 60 days. You **don't** edit `StampsRuleV1`; if you did, every old JSON row would suddenly
deserialize against new code with different semantics, silently changing the deal for programs that were
running fine. Instead you:

1. Add `StampsRuleV2` with the new `ExpiryDays` field.
2. Add `V2` to the `LoyaltyRuleVersion` enum.
3. Add one arm to the rehydration switch: `(Stamps, V2) => Deserialize<StampsRuleV2>(...)`.

Old rows stay `(Stamps, V1)` and keep deserializing into `StampsRuleV1` with the old behaviour,
*forever*, with **no data migration**. New rows are written as `V2`. The two coexist. This is the same
discipline event-sourced systems use for event upcasting, and it's what lets a long-lived product change
its rules without breaking history. The version tag is a promise: *"the shape of this data is frozen; to
change it, make a new shape."*

## Where the patterns from earlier parts show up

This one article quietly reuses the whole series:

- The rule is a **Value Object** ([Part 1](01-rich-domain-model.md)) — immutable, value-equal.
- Rules are created only through the `LoyaltyProductGroup` aggregate root's methods, which validate them
  (recall the "no rule specified" guard from Part 1), so the **aggregate stays the consistency guardian**.
- Changing a group's rules raises a `LoyaltyGroupUpdatedDomainEvent` ([Part 3](03-domain-events-outbox.md))
  so downstream caches and projections can react.

The strategy/versioning machinery isn't a separate kingdom; it sits inside the same DDD model.

## The trade-offs

Serializing strategies as JSON is powerful but has real costs — be honest about them:

- **You lose relational guarantees.** The JSON blob is opaque to the database: no foreign keys into it,
  no `WHERE FixedPercent > 5` across rules, no column constraints. If you need to *query* rule internals,
  this model fights you. (Here, rules are read by group and applied in code, so it's a fine fit.)
- **Validation moves into code, per type.** There's no schema enforcing that a `StampsRuleV1` has a
  positive `StampsToCollect`; you need per-type validators. The codebase pairs each rule with
  FluentValidation rules for exactly this reason.
- **You must keep old versions forever.** Every `V1` class you write is load-bearing as long as one row
  references it. Delete it and old programs throw on read. That's the deal you signed for zero-migration
  evolution.
- **Don't over-version.** Add a `V2` when the *shape or meaning* changes, not for cosmetic tweaks. A
  proliferation of versions with one-line differences is a smell that the rule boundaries are wrong.
- **The discriminator switch is a maintenance point.** Every new `(type, version)` needs an arm. A
  registry/factory keyed on the pair keeps it tidy as the matrix grows.

Use this pattern when rules are **many, varied in shape, and changing over time** — exactly the loyalty
domain. For two fixed rule types that never change, a couple of columns and a polymorphic method on the
aggregate is simpler and you should prefer it.

## Takeaways

- **Model volatile business rules as strategies** — one small class per rule kind, sharing a thin base.
  New rules don't touch old ones.
- **Persist polymorphic strategies as JSON + a `(type, version)` tag.** Schema-on-read recovers the
  meaning; the table stays simple.
- **Version the shape, never mutate it.** `RuleV1`/`RuleV2` + a version enum lets old and new rules
  coexist with zero data migration — the single most important trick for long-lived configurable systems.
- **Use a `[Flags]` enum** to express compatibility/combinability declaratively instead of in branching
  code.
- **Mind the costs:** opaque-to-SQL, code-side validation, and old versions you can never delete. Worth
  it when rules are many and evolving; overkill when they're few and fixed.

In **Part 5**, the finale: how this one database safely serves *many venues at once* — row-level
**multi-tenancy** with EF Core global query filters for reads and a write-side guard that makes
cross-tenant leaks impossible. [Read Part 5 →](05-multitenancy-query-filters.md)

---

*Full source: [ddd-by-example](https://github.com/richardchanjr90-cpu/ddd-by-example). The rule strategies
live in [`Core.Entities/Rules`](https://github.com/richardchanjr90-cpu/ddd-by-example/blob/main/src/Loyalty.Core.Entities/Rules) and the storage value object in
[`LoyaltyGroupRule`](https://github.com/richardchanjr90-cpu/ddd-by-example/blob/main/src/Loyalty.Core.Entities/Aggregates/LoyaltyPrograms/LoyaltyGroupRule.cs).*

---

*Written by **Richard Chan** — I build and write about Domain-Driven Design and clean architecture in .NET. This is part 4 of a five-part series; the complete, runnable source is open-source at [github.com/richardchanjr90-cpu/ddd-by-example](https://github.com/richardchanjr90-cpu/ddd-by-example). If it helped, a clap and a follow help others find it.*
