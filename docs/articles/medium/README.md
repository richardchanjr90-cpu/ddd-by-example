# Medium-ready drafts

These are paste-ready versions of the [article series](../README.md), prepared for publishing on Medium
(or dev.to / Hashnode):

- **Diagrams are embedded as images** (rendered via [mermaid.ink](https://mermaid.ink)) so they display
  directly — Medium does not render Mermaid source.
- **All repo links are absolute** (`https://github.com/richardchanjr90-cpu/ddd-by-example/...`) so they
  work once pasted out of the repo.
- Each file has a **title, subtitle, suggested tags, and author bio** block ready to go.

| Part | Draft |
|------|-------|
| 1 | [Stop Writing Anemic Domain Models](01-rich-domain-model.md) |
| 2 | [Your `enum` Is a Code Smell](02-state-machine-enumeration.md) |
| 3 | [Domain Events and the Dual-Write Bug](03-domain-events-outbox.md) |
| 4 | [Versioning Business Rules](04-versioned-rules-strategy.md) |
| 5 | [Multi-Tenancy in EF Core](05-multitenancy-query-filters.md) |

## Per-article publishing steps

1. **Paste** the Markdown into the Medium editor (it imports headings, code blocks, links, and the
   embedded diagram images automatically).
2. **Set the title and subtitle** from the `#` and `>` lines at the top, then delete those two lines and
   the tags line (they're guidance, not body copy).
3. **Add the tags** from the "Suggested Medium tags" line (Medium allows up to 5).
4. **Fix the cross-part links.** Inside these drafts, links to other parts point at sibling files
   (`02-state-machine-enumeration.md`). After you publish each part, replace those with the live Medium
   URLs so readers can navigate the series.
5. **Set a canonical link** back to the repo (Medium → Story settings → Advanced) to consolidate SEO.
6. **Publish part 1 first**, then update parts 2–5 with the real previous/next URLs.

## Image note

The diagram images are hot-linked from `mermaid.ink`. That's fine for drafts and most publishing, but if
you want them fully self-hosted, open each diagram's URL, save the PNG, and re-upload it to Medium (or
commit the PNGs under `docs/images/` and point the `![...]` links there).
